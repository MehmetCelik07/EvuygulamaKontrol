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
    public GameObject ilkPanel,kabulimg,redimg,onayPanel,g�venlikPanel;
    public int sifreSorulsunMu;
    public Text Sifretxt, KullaniciAditxt, sifreSorulmaTxt,secilenSehirtxt,ManineIdTxt;
    public string[] sehirler = { "Adana", "Ad�yaman", "Afyonkarahisar", "A�r�", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin", "Ayd�n", "Bal�kesir", "Bart�n",
            "Batman", "Bayburt", "Bilecik", "Bing�l", "Bitlis", "Bolu", "Burdur", "Bursa", "�anakkale", "�ank�r�", "�orum", "Denizli", "Diyarbak�r", "D�zce", "Edirne",
            "Elaz��", "Erzincan", "Erzurum", "Eski�ehir", "Gaziantep", "Giresun", "G�m��hane", "Hakk�ri", "Hatay", "I�d�r", "Isparta", "�stanbul", "�zmir", "Kahramanmara�",
            "Karab�k", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kilis", "K�r�kkale", "K�rklareli", "K�r�ehir", "Kocaeli", "Konya", "K�tahya", "Malatya", "Manisa", "Mardin",
            "Mersin", "Mu�la", "Mu�", "Nev�ehir", "Ni�de", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "�anl�urfa",
            "Siirt", "Sinop", "Sivas", "��rnak", "Tekirda�", "Tokat", "Trabzon", "Tunceli", "U�ak", "Van", "Yalova", "Yozgat", "Zonguldak" };

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
            sifreSorulmaTxt.text = "Her a��l��ta �ifre sorulacak";
        }
        else
        {
            sifreSorulmaTxt.text = "A��l��ta �ifre sorulmayacak";
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
        g�venlikPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
        onayPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);

    }


}
