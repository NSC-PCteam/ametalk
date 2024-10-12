using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Shuyaku がゴールに触れたとき
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Ametalkclub");
        }
    }
}
