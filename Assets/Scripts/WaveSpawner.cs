using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 0f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    public FlowFieldController gridController;

    void Update()
    {
        //if (EnemiesAlive > 0)
        //{
        //    return;
        //}

        if (waveIndex == waves.Length)
        {
            //gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        //waveCountdownText.text = string.Format("{0:00.00}", countdown);
        Debug.Log(countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }
    
    void SpawnEnemy(GameObject enemy)
    {
        GameObject newUnit1=Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        newUnit1.transform.parent = transform;
        //newUnit1.transform.position = position;
        newUnit1.GetComponent<Goblin>().GridController = gridController;
        //unitsInGame.Add(newUnit1);

    }

}
