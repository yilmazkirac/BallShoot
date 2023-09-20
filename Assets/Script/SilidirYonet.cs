using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SilidirYonet : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool butonpress;
    public GameObject Silindir;
   [SerializeField] private float DonusGucu;
   [SerializeField] private string Yon;
    public void OnPointerDown(PointerEventData eventData)
    {
        butonpress=true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        butonpress = false;
    }


    void Update()
    {
        
        if (butonpress)
        {
            if (Yon == "Sol")
            {
                Silindir.transform.Rotate(0, -DonusGucu * Time.deltaTime, 0, Space.Self);
            }
            else
            {
                Silindir.transform.Rotate(0, DonusGucu * Time.deltaTime, 0, Space.Self);
            }
           
        }
       
        
    }
}
