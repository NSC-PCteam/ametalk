using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigokunomon : MonoBehaviour
{
    public GameObject bigMush;
    public GameObject bossSkull;
    private bool trigger=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // BIGMushが倒されたらBossSkullを表示する
        if (!bigMush.activeSelf && !bossSkull.activeSelf && !trigger)
        {
            bossSkull.SetActive(true);    // BIGMushが存在しなくなったらボスを表示
            trigger = true;
        }
    }
}
