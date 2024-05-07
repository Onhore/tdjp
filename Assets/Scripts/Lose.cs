using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public GameObject lose;

    void Update()
    {
        if (PlayerStats.Gold.Score == 0)
        {
            DeathScreen();
        }
    }


    public void DeathScreen()
    {
        lose.SetActive(true);
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}