using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance = null;

    public string Ametalkclub;
    public string GameClear;
    public string GameOver;

    public void LoadAmetalkclubScreen()
    {
        SceneManager.LoadScene(Ametalkclub);
    }

    public void LoadClearScreen()
    {
        SceneManager.LoadScene(GameClear);
    }
    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene(GameOver);
    }

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
