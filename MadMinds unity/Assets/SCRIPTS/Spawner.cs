using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Wave[] waves; //waves array
    public Enemy enemy;//ref to enemy


    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive; //for next waves
    float nextSpawnTime;

    void Start()
    {
        NextWave();
    }

    void Update()
    {

        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath; //when wave1 dies, then wave two comes in
        }
    }

    void OnEnemyDeath()
    {
        print("Enemy died");
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            NextWave(); //when no enemies alive then start next wave
        }
    }

    void NextWave()
    {
        currentWaveNumber++;  // first wave num =1
        
        print("Wave: " + currentWaveNumber);
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1]; //takes from the waves array

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    [System.Serializable]
    //set info for eenmy wave

    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }

}
