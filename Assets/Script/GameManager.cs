using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    [Header("------TOP AYARLARI")]
    public GameObject[] Toplar;
    public GameObject FirePoint;
    public float Topgucu;
    int AktifTopIndex;
    public Animator _TopAtar;
    public ParticleSystem TopAtmaEfekt;
    public ParticleSystem[] TopEfektleri;
    int AktifTopEfektIndex;
    public AudioSource[] TopSesleri;
    int AktifTopSesIndex;
    public AudioSource TopAtmaSes;
    public AudioSource OyunSesi;

    [Header("------CANVAS AYARLARI")]
    [SerializeField] private int HedefTopSayisi;
    [SerializeField] private int MevcutTopSayisi;
    int GirenTopSayisi;
    public Slider LevelSlider;
    public TextMeshProUGUI KalanTopSayisi_text;

    [Header("------GENEL AYARLARI")]
    public GameObject[] Paneller;
    public TextMeshProUGUI YildizTxt;
    public TextMeshProUGUI KazandinlvlSayitxt;
    public TextMeshProUGUI KaybettinlvlSayitxt;
    public Renderer KovaSeffaf;
    float kovaBaslangicDegeri;
    float kovaMevcutDeger;
    string levelAd;
    private void Start()
    {
        levelAd = SceneManager.GetActiveScene().name;
        AktifTopSesIndex = 0;
        kovaBaslangicDegeri = .5f;
        kovaMevcutDeger = .25f / HedefTopSayisi;
        AktifTopEfektIndex = 0;
        LevelSlider.maxValue = HedefTopSayisi;
        KalanTopSayisi_text.text = MevcutTopSayisi.ToString();
        KovaSeffaf.material.SetTextureScale("_MainTex", new Vector2(1f, kovaBaslangicDegeri));
    }

    public void TopGirdi()
    {
        kovaBaslangicDegeri -= kovaMevcutDeger;
        KovaSeffaf.material.SetTextureScale("_MainTex", new Vector2(1f, kovaBaslangicDegeri));
        GirenTopSayisi++;
        LevelSlider.value = GirenTopSayisi;

        TopSesleri[AktifTopSesIndex].Play();
        AktifTopSesIndex++;

        if (AktifTopSesIndex == TopSesleri.Length - 1)
            AktifTopSesIndex = 0;
        if (GirenTopSayisi == HedefTopSayisi)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Yildiz", PlayerPrefs.GetInt("Yildiz") + 15);
            YildizTxt.text = PlayerPrefs.GetInt("Yildiz").ToString();

            OyunSonuIslem(1);
        }
        int sayi = 0;
        foreach (var item in Toplar)
        {
            if (item.activeInHierarchy)
            {
                sayi++;
            }
        }
        if (sayi == 0)
        {
            if (MevcutTopSayisi == 0 && GirenTopSayisi != HedefTopSayisi)
            {
                OyunSonuIslem(2);
            }
            if ((MevcutTopSayisi + GirenTopSayisi) < HedefTopSayisi)
            {
                OyunSonuIslem(2);
            }
        }
    

    }
    void OyunSonuIslem(int panelSayi)
    {
        OyunSesi.Stop();
        Paneller[panelSayi].SetActive(true);
        KaybettinlvlSayitxt.text = "LEVEL : " + levelAd;
        Time.timeScale = 0;
    }
    public void TopGirmedi()
    {
        if (MevcutTopSayisi == 0)
        {
            OyunSonuIslem(2);
        }
        if ((MevcutTopSayisi + GirenTopSayisi) < HedefTopSayisi)
        {
            OyunSonuIslem(2);
        }

    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
               
                if (MevcutTopSayisi > 0)
                {
                    TopAtmaSes.Play();
                    MevcutTopSayisi--;
                    KalanTopSayisi_text.text = MevcutTopSayisi.ToString();
                    _TopAtar.Play("topatar");
                    TopAtmaEfekt.Play();
                    Toplar[AktifTopIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
                    Toplar[AktifTopIndex].SetActive(true);
                    Toplar[AktifTopIndex].GetComponent<Rigidbody>().AddForce(Toplar[AktifTopIndex].transform.TransformDirection(90, 90, 0) * Topgucu, ForceMode.Force);

                    if (Toplar.Length - 1 == AktifTopIndex)
                        AktifTopIndex = 0;
                    else
                        AktifTopIndex++;
                }

            }
        }

    }
    public void YokOlmaEfekt(Vector3 TopPos, Color Renk)
    {
        TopEfektleri[AktifTopIndex].transform.position = TopPos;
        var main = TopEfektleri[AktifTopIndex].main;
        main.startColor = Renk;
        TopEfektleri[AktifTopIndex].gameObject.SetActive(true);
        AktifTopEfektIndex++;
        if (AktifTopEfektIndex == TopEfektleri.Length - 1)
            AktifTopEfektIndex = 0;


    }
    public void OyunuDurdur()
    {
        Paneller[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void DurdurmaPaneliButonlari(string islem)
    {
        switch (islem)
        {
            case "Devamet":
                Paneller[0].SetActive(false);
                Time.timeScale = 1;
                break;
            case "Ayarlar":
                Time.timeScale = 1;
                break;
            case "Cikis":

                break;
            case "Tekrar":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;

            case "Sonraki":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
