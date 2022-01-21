using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemy : MonoBehaviour
{
  
    public string lastHit;
    public float speed = 10;
    private int score;
    private Transform target;
    private Rigidbody rb;
    public GameObject player_object;
    private player_input player_script;

    void Start()
    {
        player_script = player_object.GetComponent<player_input>();
    }   

    void Update()
    {
        if (this.gameObject.transform.position.y < -7)//Karakter platform seviyesinin altina indiginde yok edilme
        {
            Destroy(this.gameObject);
            player_script.playersLength--;// Anaoyuncunun componentindeki oyuncu sayisini azaltma
            GameObject.Find(lastHit).transform.localScale += new Vector3(0.3f, 0.3f, 0.3f) * score; //Kendisine son vuran karaktere puani kadar boost verme ve puanini aktarma
            if (GameObject.Find(lastHit).GetComponent<enemy>() != null) { GameObject.Find(lastHit).GetComponent<enemy>().score+=score; }
            else { GameObject.Find(lastHit).GetComponent<player_input>().score+=score; }         
           
        }
        this.gameObject.GetComponentInChildren<TextMeshPro>().text = "" + score; 
        target = FindTarget();
        rb = GetComponent<Rigidbody>();

    }
    public Transform FindTarget()
    {
        //En yakin boost objesini hedef alma
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("food");
        float minDistance = Mathf.Infinity;
        Transform closest;

        if (candidates.Length == 0)
            return null;

        closest = candidates[0].transform;
        for (int i = 0; i < candidates.Length; ++i)
        {
            float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                closest = candidates[i].transform;
                minDistance = distance;
            }
        }
        return closest;
    }
    private void FixedUpdate()
    {
        //Hedef bulundugunda hedefe hareket etme 
        if (target != null)
        {
            transform.LookAt(target);
            Vector3 direction = (target.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Carpisma yapilan obje botlardan biri veya oyuncu oldugunda  
        if (other.gameObject.tag == "Player")
        {
            lastHit = other.gameObject.name;//Kendisine carpan son obje (Bot oyunu kaybettiginde topladigi boostlar bu objeye gececek)
            if (this.gameObject.transform.localScale.x - other.gameObject.transform.localScale.x > 0) {
                var power = other.gameObject.transform.localScale.x - this.gameObject.transform.localScale.x;//Carpilan objeye uygulanacak gucun ve yonun hesaplamasi(guc karakterler arasinda boyut farki)
                var force = transform.position - other.transform.position;
                force.Normalize();
                other.gameObject.GetComponent<Rigidbody>().AddForce(force * 700 * power);
            }
        }
        if (other.gameObject.tag == "food")
        {
            //Boost objesinin yok edilmesi, score yukselmesi ve obje boyutunun arttirilmasi
            Destroy(other.gameObject);
            score++;
            this.gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}
