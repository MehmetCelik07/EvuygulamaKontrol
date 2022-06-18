using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FirstScan : MonoBehaviour
{
    public Dropdown sehirlerDropDown;
    public List<Dropdown.OptionData> optionDatas;
    public Text kullaniciadi, sifre,makineid;
    public GameObject ilkPanel,kabulimg,redimg,onayPanel,güvenlikPanel;
    public int sifreSorulsunMu;
    public Text Sifretxt, KullaniciAditxt, sifreSorulmaTxt,secilenSehirtxt,ManineIdTxt;
    public string[] sehirler = { "Adana", "Adýyaman", "Afyonkarahisar", "Aðrý", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin", "Aydýn", "Balýkesir", "Bartýn",
            "Batman", "Bayburt", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankýrý", "Çorum", "Denizli", "Diyarbakýr", "Düzce", "Edirne",
            "Elazýð", "Erzincan", "Erzurum", "Eskiþehir", "Gaziantep", "Giresun", "Gümüþhane", "Hakkâri", "Hatay", "Iðdýr", "Isparta", "Ýstanbul", "Ýzmir", "Kahramanmaraþ",
            "Karabük", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kilis", "Kýrýkkale", "Kýrklareli", "Kýrþehir", "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Mardin",
            "Mersin", "Muðla", "Muþ", "Nevþehir", "Niðde", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "Þanlýurfa",
            "Siirt", "Sinop", "Sivas", "Þýrnak", "Tekirdað", "Tokat", "Trabzon", "Tunceli", "Uþak", "Van", "Yalova", "Yozgat", "Zonguldak" };

    private void Start()
    {
        optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < sehirler.Length; i++)
        {
            Dropdown.OptionData sehir = new Dropdown.OptionData(sehirler[i]);
            optionDatas.Add(sehir);
        }
        sehirlerDropDown.AddOptions(optionDatas);
        PlayerPrefs.SetInt("ilkAcilismi", 1);
        sifreSorulsunMu = 0;

    }
    public void sifreSor()
    {
        if (sifreSorulsunMu ==0)
        {
            sifreSorulsunMu = 1;
            kabulimg.GetComponent<RectTransform>().DOScale(1, 0.2f);
            redimg.GetComponent<RectTransform>().DOScale(0, 0.2f);
        }
        else if(sifreSorulsunMu==1)
        {
            sifreSorulsunMu = 0;
            kabulimg.GetComponent<RectTransform>().DOScale(0, 0.2f);
            redimg.GetComponent<RectTransform>().DOScale(1, 0.2f);
        }
    }
    public void kaydet()
    {
        onayPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
        KullaniciAditxt.text = kullaniciadi.text;
        Sifretxt.text = sifre.text;
        secilenSehirtxt.text = sehirler[sehirlerDropDown.value];
        ManineIdTxt.text = makineid.text;
        if (sifreSorulsunMu ==1)
        {
            sifreSorulmaTxt.text = "Her açýlýþta þifre sorulacak";
        }
        else
        {
            sifreSorulmaTxt.text = "Açýlýþta þifre sorulmayacak";
        }
    }
    public void reddet()
    {
        onayPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
    }
    public void onayla()
    {
        string secilenSehir = sehirler[sehirlerDropDown.value];
        PlayerPrefs.SetString("SecilenSehir", secilenSehir);
        PlayerPrefs.SetString("kullaniciAdi", kullaniciadi.text);
        PlayerPrefs.SetString("sifre", sifre.text);
        PlayerPrefs.SetString("makineid", makineid.text);
        PlayerPrefs.SetInt("ilkAcilismi", 0);
        PlayerPrefs.SetInt("sifreSorulsunMu", sifreSorulsunMu);
        ilkPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
        güvenlikPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
        onayPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);

    }


}
