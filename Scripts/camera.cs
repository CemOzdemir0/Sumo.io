using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;       
    private Vector3 main_position = new Vector3(0,30,-20);
    private Vector3 offset;           

    void Start()
    {
        //Oyuncu ve kamera konumu arasýndaki farkin hesaplanmasi ve saklanmasi
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        if (player == null)
        {//Karakter yok oldugunda kalan oyuncularin izlenmesi icin 
            transform.position = main_position;
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
    public void Restart()
    {
        //Tekrar oyna butonu
        Application.LoadLevel(Application.loadedLevel);
    }
}
