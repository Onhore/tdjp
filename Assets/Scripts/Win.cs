using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject win;

    void Update()
    {
        if (PlayerStats.Gold.Score > 800)
        {
            DeathScreen();
        }
    }


    public void DeathScreen()
    {
        win.SetActive(true);
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}