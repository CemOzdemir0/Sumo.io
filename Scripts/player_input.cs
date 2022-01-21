using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player_input : MonoBehaviour
{
    public int score;
    public GameObject gameover_panel;
    public TextMeshProUGUI gameover_text;
    public float speed;
    public float rotationSpeed;
    public int playersLength;

    void Start()
    {
        //Kazanmamiz icin elenmesi gereken oyuncu sayisi
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Player");
        playersLength = enemys.Length-1;
        
    }

    void Update()
    {
        
        this.gameObject.GetComponentInChildren<TextMeshPro>().text = "" + score;

        Debug.Log(playersLength);

        //Kazanip kaybetme durumu
        if (this.gameObject.transform.position.y < -7) {
            gameover_text.text = "KAYBETTIN";
            gameover_panel.SetActive(true);
            Destroy(this.gameObject);
        }
        if (playersLength == 0) {
            Debug.Log("Dusman Kalmadi");
            gameover_text.text = "KAZANDIN";
            gameover_panel.SetActive(true);
            Destroy(this.gameObject);
        }

      
        //Mobil uyumsuz karakter kontrolleri
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //Carpisma durumlari 
        if (other.gameObject.tag == "food")
        {
            Destroy(other.gameObject);
            score++;    
            this.gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
        }
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.transform.localScale.x - this.gameObject.transform.localScale.x < 0)
            {
                var power = other.gameObject.transform.localScale.x - this.gameObject.transform.localScale.x;
                var force = transform.position - other.transform.position;
                force.Normalize();
                other.gameObject.GetComponent<Rigidbody>().AddForce(force * 700 * power);
            }
        }
    }
}
