using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manual : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {// 始まった時に実行する関数	
        { // ボタンが押された時、StartGame関数を実行
            gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
        } // StartGame関数
        void StartGame()
        { // GameSceneをロード
            SceneManager.LoadScene("Manual");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
