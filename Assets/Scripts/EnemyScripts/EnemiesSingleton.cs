using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class EnemiesSingleton : MonoBehaviour
{

    private static EnemiesSingleton instance;
    public static EnemiesSingleton Instance {

        get {

            return instance ?? (instance = new GameObject("EnemiesSingleton").AddComponent<EnemiesSingleton>());

        }

    }

    public List<GameObject> enemiesSpawned = new List<GameObject>();

    public delegate void EnemyKilled(GameObject enemy);
    public EnemyKilled enemyKilled;

    public delegate void SpawnAnotherWave();
    public SpawnAnotherWave spawnAnotherWave;


    public int waveMax = 3;
    public int currentWave = 1;
    public bool WavesEnded;


    public void AddEnemy(GameObject enemy) {

        if(!enemiesSpawned.Contains(enemy)) {

            enemiesSpawned.Add(enemy);

        }

    }

    public void RemoveEnemy(GameObject enemy) {

        if (enemiesSpawned.Contains(enemy)) {

            enemiesSpawned.Remove(enemy);

            if(enemiesSpawned.Count == 0) {

                if(currentWave < waveMax) {

                    currentWave++;
                    spawnAnotherWave();

                }

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyKilled += RemoveEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        WavesEnd();
    }

    public void WavesEnd()
    {


        if (enemiesSpawned.Count == 0)
        {

            if (currentWave == waveMax)
            {

                WavesEnded = true;

            }

        }

    }
}
