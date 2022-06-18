#include <DHT.h>  // Including library for dht
#include "ThingSpeak.h" 
#include <ESP8266WiFi.h>
#include<ESP8266WebServer.h>
 
String apiKey = "8KSDS96D2XD68GA7";    
unsigned long channel =1626289;
const char *ssid =  "Redmi 22";     
const char *pass =  "240240pp";
const char* server = "api.thingspeak.com";
unsigned long ledkanal=1580443;
int led1,led2,led3,led4,lamba;
unsigned int value;
WiFiClient client;  

 
#define DHTPIN 13     
DHT dht(DHTPIN, DHT11);
int buzzer = 15;
int trigPin = 12;
int echoPin = 14;

long sure;

long uzaklik;
 

 
void setup() 
{
       
       Serial.begin(115200);
       delay(10);
       dht.begin();
       Serial.println("Connecting to ");
       Serial.println(ssid);
       pinMode(D0,OUTPUT);
       pinMode(D1,OUTPUT);
       pinMode(D2,OUTPUT);
       pinMode(D3,OUTPUT);
       digitalWrite(D0,LOW);
       digitalWrite(D1,LOW);
       digitalWrite(D2,LOW);
       digitalWrite(D3,LOW);
       pinMode(trigPin, OUTPUT);

       pinMode(echoPin,INPUT);


       

       
 
 
       WiFi.begin(ssid, pass);
 
      while (WiFi.status() != WL_CONNECTED) 
     {
            delay(500);
            Serial.print(".");
     }
      Serial.println("");
      Serial.println("WiFi connected");

       Serial.println(WiFi.localIP());    // print the wifi local ip

 ThingSpeak.begin(client);      // connect the client to the thingSpeak server
      
 
}
 
void loop() 
{
  
      digitalWrite(trigPin, LOW);
      delayMicroseconds(5);
      digitalWrite(trigPin, HIGH);
      delayMicroseconds(10);
      digitalWrite(trigPin, LOW); 
      sure = pulseIn(echoPin, HIGH); 
      uzaklik= sure /29.1/2;
      Serial.print(uzaklik); 

      
      float h = dht.readHumidity();
      float t = dht.readTemperature();
      float g = analogRead(A0);
      
              if (isnan(h) || isnan(t)|| isnan(g)) 
                 {
                     Serial.println("sensörlerin herhangibi birinde hata meydana geldi.");
                      return;
                 }
 
                         if (client.connect(server,80))   
                      {  
                            
                             String postStr = apiKey;
                             postStr +="&field1=";
                             postStr += String(t);
                             postStr +="&field2=";
                             postStr += String(h);
                             postStr += "&field3=";
                             postStr += String(g/1023*100);
 
                             client.print("POST /update HTTP/1.1\n");
                             client.print("Host: api.thingspeak.com\n");
                             client.print("Connection: close\n");
                             client.print("X-THINGSPEAKAPIKEY: "+apiKey+"\n");
                             client.print("Content-Type: application/x-www-form-urlencoded\n");
                             client.print("Content-Length: ");
                             client.print(postStr.length());
                             client.print("\n\n");
                             client.print(postStr);
 
                             Serial.print("Sicaklik: ");
                             Serial.print(t);
                             Serial.print("nem: ");
                             Serial.print(h);
                             Serial.print("Gaz seviyesi: ");
                             Serial.println(g);
                        }
                        if(g>550 || h>60 || t>40 )
                        {
                          tone(buzzer,1000,200);
                        }
                        else
                        {
                          noTone(buzzer);
                        }
                        if(uzaklik<3)
                        {
                           tone(buzzer,800,1000);  
                        }
                        else if(uzaklik<5)
                        {
                           tone(buzzer,800,800);  
                        }
                        else if(uzaklik<7)
                        {
                           tone(buzzer,800,600);  
                        }
                        else if(uzaklik<9)
                        {
                           tone(buzzer,800,500);  
                        }
                        else if(uzaklik<11)
                        {
                           tone(buzzer,800,400);  
                        }
                        else if(uzaklik<13)
                        {
                           tone(buzzer,800,300);  
                        }
                        else if(uzaklik<15)
                        {
                           tone(buzzer,800,200);  
                        }
                        
                        
                        else
                        {
                           noTone(buzzer);  
                        }
                        
           


 led1=ThingSpeak.readFloatField(ledkanal,1); 
 if(led1==1)
 {
   digitalWrite(D0,HIGH);
 }
 else if(led1==0)
 {
   digitalWrite(D0,LOW);
 }    
  led2=ThingSpeak.readFloatField(ledkanal,2); 
 if(led2==1)
 {
   digitalWrite(D1,HIGH);
 }
 else if(led2==0)
 {
   digitalWrite(D1,LOW);
 }   
  led3=ThingSpeak.readFloatField(ledkanal,3); 
 if(led3==1)
 {
   digitalWrite(D2,HIGH);
 }
 else if(led3==0)
 {
   digitalWrite(D2,LOW);
 }   
  led4=ThingSpeak.readFloatField(ledkanal,4); 
 if(led4==1)
 {
   digitalWrite(D3,HIGH);
 }
 else if(led4==0)
 {
   digitalWrite(D3,LOW);
 }   
  lamba=ThingSpeak.readFloatField(ledkanal,5); 
 if(lamba==1)
 {
   digitalWrite(D4,HIGH);
 }
 else if(lamba==0)
 {
   digitalWrite(D4,LOW);
 }   
          client.stop();
 
          Serial.println("Waiting...");
  
  delay(500);
   




}
