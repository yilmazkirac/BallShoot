using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("------TOP AYARLARI")]
    public GameObject[] Toplar;
    public GameObject FirePoint;
    public float Topgucu;
    int AktifTopIndex;



    [Header("------TOP AYARLARI")]
    [SerializeField] private int HedefTopSayisi;
    [SerializeField] private int MevcutTopSayisi;
    int GirenTopSayisi;
    public Slider LevelSlider;
    public TextMeshProUGUI KalanTopSayisi_text;

    private void Start()
    {
        LevelSlider.maxValue = HedefTopSayisi;
        KalanTopSayisi_text.text = MevcutTopSayisi.ToString();
    }

    public void TopGirdi() 
    {
        GirenTopSayisi++;
        LevelSlider.value= GirenTopSayisi;

        if (GirenTopSayisi==HedefTopSayisi)
        {
            //KazandinPaneli/TopAtmayiKitle

        }
        if (MevcutTopSayisi==0&&GirenTopSayisi!=HedefTopSayisi)
        {
            //KaybettinPaneli/TopAtmayiKitle
        }

    }
    public void TopGirmedi()
    {
        if (MevcutTopSayisi==0)
        {
            //KaybettinPaneli
        }
        if ((MevcutTopSayisi+GirenTopSayisi)<HedefTopSayisi)
        {
            //KaybettinPaneli
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (MevcutTopSayisi>0)
            {
                MevcutTopSayisi--;
                KalanTopSayisi_text.text = MevcutTopSayisi.ToString();
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
