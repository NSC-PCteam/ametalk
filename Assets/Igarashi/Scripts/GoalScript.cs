using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // シーン内の GameManager を探す
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Shuyaku がゴールに触れたとき
        if (collision.CompareTag("Player"))
        {
            gameManager.GoalReached();
        }
    }
}
