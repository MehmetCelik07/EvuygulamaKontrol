using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.UI;
using System.Xml.Linq;
using System.Linq;
using System.Globalization;
using DG.Tweening;
using System.Xml;


public class thingspeakdeneme : MonoBehaviour
{
    public Text disSicaklikTxt, icSicaklikTxt, disNemTxt, icNemTxt, hissedilenSicaklikTxt, havadurumuTxt;
    public Text usdTxt, eurTxt, paundTxt, yenTxt, rubTxt, yuanTxt, manatTxt, wonTxt,tarihTxt,testTxt,vakaTxt,deathTxt,iyilesenTxt,NotAl;
    public Slider disSicaklikSlider, DisNemSlider, hissedilenSlider, icNemSlider, icSicaklikSlider;
    public GameObject[] toplar;
    public GameObject borsaPanel,covidPanel,notPanel,bekleyinPanel,kontrolPanel,ilkPanel,GüvenlikPanel;
    public InputField defter;
    public GameObject[] durdurBtnlar;
    public void Start()
    {
        StartCoroutine(Havadegercek());
        StartCoroutine(thingSpeakDegerCek());
        StartCoroutine(Borsaa());
        AcilisAyarla();
    }
   
    public void ledYak(int hangiled)
    {
        int durdur=0;
        bekleyinPanel.GetComponent<RectTransform>().DOScale(1,0);
        do
        {
            
            string adres = "https://api.thingspeak.com/update?api_key=C3SK99YGJ46U3ZSS&field" +hangiled.ToString() + "=1";
            WebRequest istek = HttpWebRequest.Create(adres);
            WebResponse cevap;
            cevap = istek.GetResponse();
            StreamReader gelenbilgiler = new StreamReader(cevap.GetResponseStream());
            string gelen = gelenbilgiler.ReadToEnd();
            durdur = int.Parse(gelen);
        } while (durdur<1);

      
        durdurBtnlar[hangiled - 1].GetComponent<RectTransform>().DOScale(1, 0.3f);
        bekleyinPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
    }
    public void ledKapa(int hangiLed)
    {
        int durdur=0;
        bekleyinPanel.GetComponent<RectTransform>().DOScale(1, 0);
        
        
        do
        {
            
            string adres = "https://api.thingspeak.com/update?api_key=C3SK99YGJ46U3ZSS&field" + hangiLed.ToString() + "=0";
            WebRequest istek = HttpWebRequest.Create(adres);
            WebResponse cevap;
            cevap = istek.GetResponse();
            StreamReader gelenbilgiler = new StreamReader(cevap.GetResponseStream());
            string gelen = gelenbilgiler.ReadToEnd();
            durdur = int.Parse(gelen);
        } while (durdur < 1);


        durdurBtnlar[hangiLed - 1].GetComponent<RectTransform>().DOScale(0, 0.3f);
        bekleyinPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
    }
    IEnumerator Havadegercek()
    {
        do
        {
            string sehir = PlayerPrefs.GetString("SecilenSehir");
            string adres = "https://api.openweathermap.org/data/2.5/weather?q=" +sehir + "&mode=xml&lang=tr&units=metric&appid=7e4c02f57e72b2fbc735614ac1d3b0a1";
            XDocument durum = XDocument.Load(adres);
            var sicaklik = durum.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            var hissedilenSicaklik = durum.Descendants("feels_like").ElementAt(0).Attribute("value").Value;
            var nem = durum.Descendants("humidity").ElementAt(0).Attribute("value").Value;
            var havaDurumu = durum.Descendants("weather").ElementAt(0).Attribute("value").Value;
            disSicaklikTxt.text = sicaklik.ToString();
            disSicaklikSlider.value = float.Parse(sicaklik, CultureInfo.InvariantCulture.NumberFormat);
            hissedilenSicaklikTxt.text = hissedilenSicaklik.ToString();
            hissedilenSlider.value = float.Parse(hissedilenSicaklik, CultureInfo.InvariantCulture.NumberFormat);
            float yüzdeNem = 100- float.Parse(nem, CultureInfo.InvariantCulture.NumberFormat);
            disNemTxt.text = yüzdeNem.ToString();
            DisNemSlider.value = yüzdeNem;
            havadurumuTxt.text = "Hava durumu : " + havaDurumu.ToString();
            yield return new WaitForSeconds(10);
        } while (true); 


    }
    IEnumerator thingSpeakDegerCek()
    {
        do
        {
            string makid = PlayerPrefs.GetString("makineid");
            string adres = "https://api.thingspeak.com/channels/"+ makid + "/feeds/last.xml";
            XDocument durum = XDocument.Load(adres);
            var sicaklik = durum.Descendants("field1").ElementAt(0).Value;
            var nem = durum.Descendants("field2").ElementAt(0).Value;
            var gaz = durum.Descendants("field3").ElementAt(0).Value;
            icSicaklikTxt.text = sicaklik.ToString();
            icSicaklikSlider.value = float.Parse(sicaklik, CultureInfo.InvariantCulture.NumberFormat);
            icNemTxt.text = nem.ToString();
            icNemSlider.value = float.Parse(nem, CultureInfo.InvariantCulture.NumberFormat);
            float topGaz = float.Parse(gaz, CultureInfo.InvariantCulture.NumberFormat);
            if (topGaz>=55)
            {
                topAc(6);
            }
            else if (topGaz >= 51)
            {
                topAc(5);
            }
            else if (topGaz >= 45)
            {
                topAc(4);
            }
            else if (topGaz >= 42)
            {
                topAc(3);
            }
            else if (topGaz >= 38)
            {
                topAc(2);
            }
            else
            {
                topAc(1);
            }
           

            yield return new WaitForSeconds(1);
        } while (true);
    }

    void topAc(int kacTane)
    {
        for (int i = 0; i < kacTane; i++)
        {
            toplar[i].GetComponent<RectTransform>().DOScale(1, 0.1f);
        }
    }

    IEnumerator Borsaa()
    {

        while (true)
        {
            string merkezBorsasi = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(merkezBorsasi);

            string usd = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='USD'] / BanknoteSelling").InnerXml;
            string eur = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='EUR'] / BanknoteSelling").InnerXml;
            string paund = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='GBP'] / BanknoteSelling").InnerXml;
            string yen = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='JPY'] / BanknoteSelling").InnerXml;
            string rub = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='RUB'] / ForexSelling").InnerXml;
            string yuan = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='CNY'] / ForexSelling").InnerXml;
            string manat = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='AZN'] / ForexSelling").InnerXml;
            string won = xmlDoc.SelectSingleNode("Tarih_Date / Currency[@Kod ='KRW'] / ForexSelling").InnerXml;

            usdTxt.text = usd;
            eurTxt.text = eur;
            paundTxt.text = paund;
            yenTxt.text = yen;
            rubTxt.text = rub;
            yuanTxt.text = yuan;
            manatTxt.text = manat;
            wonTxt.text = won;

            string[] jsonVerileri, bugunkiKoronaCozumle;

            WebClient wc = new WebClient();
            var json = wc.DownloadString("https://raw.githubusercontent.com/ozanerturk/covid19-turkey-api/master/dataset/timeline.json");

            jsonVerileri = json.ToString().Split('{');


            bugunkiKoronaCozumle = jsonVerileri[jsonVerileri.Length - 1].Split('"');

            tarihTxt.text = bugunkiKoronaCozumle[3];
            testTxt.text = bugunkiKoronaCozumle[31];
            vakaTxt.text = bugunkiKoronaCozumle[35];
            deathTxt.text = bugunkiKoronaCozumle[51];
            iyilesenTxt.text = bugunkiKoronaCozumle[55];
           
            yield return new WaitForSeconds(10);

        }



        
    }

    public void notKaydet()
    {
        
        string AlinanNot = NotAl.text;
        PlayerPrefs.SetString("Notlar", AlinanNot);
        defter.text = PlayerPrefs.GetString("Notlar");
    }
    public void panelKontrol(string hangiPanel)
    {
        if (hangiPanel == "kapat")
        {
            borsaPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            covidPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            notPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            kontrolPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            defter.text = PlayerPrefs.GetString("Notlar");
        }
        else if (hangiPanel =="borsa")
        {
            borsaPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
            covidPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            notPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            kontrolPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
        }
        else if (hangiPanel == "covid")
        {
            covidPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
            borsaPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            notPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            kontrolPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
        }
        else if (hangiPanel == "not")
        {
            notPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
            defter.text = PlayerPrefs.GetString("Notlar");
            borsaPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            covidPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            kontrolPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
        }
        else if (hangiPanel == "kontrol")
        {
            kontrolPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
            borsaPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            covidPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
            notPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);

        }

    }
    public void AcilisAyarla()
    {
        if (PlayerPrefs.GetInt("ilkAcilismi")==0)
        {
            ilkPanel.GetComponent<RectTransform>().DOScale(0, 0);
            if (PlayerPrefs.GetInt("sifreSorulsunMu")==0)
            {
                GüvenlikPanel.GetComponent<RectTransform>().DOScale(0, 0);
            }
        }
    }

   
}
