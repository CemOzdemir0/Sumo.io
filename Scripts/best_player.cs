using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class best_player : MonoBehaviour
{
    private int bestScore = 0;
    public TextMeshProUGUI bestPlayerText ;
   
    void Start()
    {
        
    }

  
    void Update()
    {
        //Oyuncular arasinda en yuksek skor sahibini bulma
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < candidates.Length; ++i)
        {
            if (int.Parse(candidates[i].GetComponentInChildren<TextMeshPro>().text) > bestScore) 
            {
                bestScore = int.Parse(candidates[i].GetComponentInChildren<TextMeshPro>().text);
                bestPlayerText.text = ("Yuksek Skor : "+ candidates[i].name);
            }         
        }
    }
}
