using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    bool isGet;
    float lifeTime = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        if (isGet)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <=0)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
     {
        if (!isGet && other.CompareTag("Player"))
        {
            isGet = true;
            transform.position += Vector3.up * 1.5f;
        }
     }
}
