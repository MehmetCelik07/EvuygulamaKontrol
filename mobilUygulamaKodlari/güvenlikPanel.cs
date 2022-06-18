using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class güvenlikPanel : MonoBehaviour
{
    public GameObject güvenlikPaneli, hataPanel,onayBtn,ilkPanel;
    public Text KullaniciAdi, sifre,HataTxt;
    public void girisYap()
    {
        if (PlayerPrefs.GetString("kullaniciAdi")==KullaniciAdi.text && PlayerPrefs.GetString("sifre") == sifre.text)
        {
            güvenlikPaneli.GetComponent<RectTransform>().DOScale(0, 0.3f);

        }
        else
        {
            HataTxt.text = "Yanlış kullanıcı adı veya şifre girdiniz";
            hataPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
        }
    }
    public void kappa()
    {
        
        hataPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
        onayBtn.GetComponent<RectTransform>().DOScale(0, 0.3f);
    }
    public void kurtarma()
    {
        HataTxt.text = "Tüm kullanıcı verileri silinecek yeni bir kullanıcı adı ve şifre oluşturulacak emin misiniz";
        hataPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
        onayBtn.GetComponent<RectTransform>().DOScale(1, 0.3f);
    }
    public void kurtar()
    {
        PlayerPrefs.DeleteAll();
        ilkPanel.GetComponent<RectTransform>().DOScale(1, 0.3f);
        güvenlikPaneli.GetComponent<RectTransform>().DOScale(0, 0.3f);
        hataPanel.GetComponent<RectTransform>().DOScale(0, 0.3f);
        onayBtn.GetComponent<RectTransform>().DOScale(0, 0.3f);
    }
    
}
