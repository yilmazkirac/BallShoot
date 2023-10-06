using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    public GameManager _GameManager;
    Rigidbody rb;
    Renderer renk;
    private void Start()
    {
       renk = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kova"))
        {
            TeknikIslem();
            _GameManager.TopGirdi();
        }
       else if (other.CompareTag("AltObje"))
        {
            TeknikIslem();
            _GameManager.TopGirmedi();
        }
      
    }

    void TeknikIslem()
    {
        _GameManager.YokOlmaEfekt(gameObject.transform.position, renk.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}