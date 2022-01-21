using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scores : MonoBehaviour
{
    public GameObject camera;
    void Start()
    {
        
    }

    void Update()
    {
        //Karakterlerin uzerindeki puanlarin her zaman kameraya bakmasi
        transform.LookAt(this.gameObject.transform.position -  (camera.transform.position - this.gameObject.transform.position)); 
    }
}
