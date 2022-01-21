using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_spawn : MonoBehaviour
{
    public float timeRemaining = 0.5f;
    public bool foodSpawned = false;
    [SerializeField]
    private GameObject food;
    void Start()
    {  
        SpawnFood();   
    }
    private void SpawnFood() {    
            //Verilen vektorel aralik kullanilarak boost objelerinin olusmasi
            Vector3 foodPosition = new Vector3(Random.Range(-40f, 40f),7f, Random.Range(-40f, 40f));
            Instantiate(food, foodPosition, Quaternion.identity);
            foodSpawned= true;     
    }
    void Update()
    {
        //Verilen sure doldugunda boost olusturma ve zamanlayici sifirlama
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else { 
            SpawnFood();
            timeRemaining = 0.5f;  
        }
    }
}
