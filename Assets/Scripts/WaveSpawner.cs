using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Flyweight;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{

    public int EnemiesAlive =0;
    public int enemiesCount;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 0f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    public FlowFieldController gridController;

    public UnityEvent Win;

    void Update()
    {
        enemiesCount = FlyweightFactory.Instance.pools.ContainsKey(typeof(GoblinSettings)) ? FlyweightFactory.Instance.goblinCount - FlyweightFactory.Instance.pools[typeof(GoblinSettings)].CountInactive : -1;
        if (EnemiesAlive > 0 && enemiesCount > 0)
            return;

        if (waveIndex == waves.Length)
        {
            Win.Invoke();
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
        //Debug.Log(countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy, spawnPoint.position);
            SpawnEnemy(wave.enemy, spawnPoint.position+new Vector3(0,0,2));
            SpawnEnemy(wave.enemy, spawnPoint.position+new Vector3(0,0,-2));
            SpawnEnemy(wave.enemy, spawnPoint.position+new Vector3(2,0,0));
            SpawnEnemy(wave.enemy, spawnPoint.position+new Vector3(-2,0,0));
            yield return new WaitForSeconds(wave.rate);
        }

        waveIndex++;
    }
    
    void SpawnEnemy(GoblinSettings s, Vector3 position)
    {
        Goblin enemy = Flyweight.FlyweightFactory.Spawn(s);
        
        enemy.transform.position = position;
        enemy.transform.parent = transform;
        //newUnit1.transform.position = position;
        //enemy.GetComponent<Goblin>().GridController = gridController;
        //unitsInGame.Add(newUnit1);

    }

}
