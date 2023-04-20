using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public int numEnemiesInWave = 5;
    public float spawnRadius = 5f;
    public float timeBetweenWaves = 5f;
    public float minDistanceBetweenEnemies = 1f;
    public bool wavesCleared = false;

    private int currentWave = 1;
    private int maxWaves;
    public int currentNight = 0;
    void Start()
    {
        //StartCoroutine(SpawnEnemies());
    }
    void increaseNight()
    {
        currentNight += 1;
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        maxWaves = currentNight;
        while (currentWave <= maxWaves)
        {
            int numEnemiesToSpawn = numEnemiesInWave * currentWave;
            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                GameObject enemyPrefab;
                if (Random.Range(0, 2) == 0)
                {
                    enemyPrefab = enemyPrefab1;
                }
                else
                {
                    enemyPrefab = enemyPrefab2;
                }

                float angle = Random.Range(0f, Mathf.PI * 2f);
                Vector3 spawnPosition3D = transform.position + new Vector3(Mathf.Sin(angle) * spawnRadius, 20f, Mathf.Cos(angle) * spawnRadius);

                // Check distance from other enemies
                bool tooClose = true;
                while (tooClose)
                {
                    tooClose = false;
                    foreach (var enemy in GameObject.FindGameObjectsWithTag("enemy"))
                    {
                        if (Vector3.Distance(enemy.transform.position, spawnPosition3D) < minDistanceBetweenEnemies)
                        {
                            tooClose = true;
                            break;
                        }
                    }

                    if (tooClose)
                    {
                        angle = Random.Range(0f, Mathf.PI * 2f);
                        spawnPosition3D = transform.position + new Vector3(Mathf.Sin(angle) * spawnRadius, 20f, Mathf.Cos(angle) * spawnRadius);
                    }
                }

                GameObject enemyIns = Instantiate(enemyPrefab, spawnPosition3D, enemyPrefab.transform.rotation);
                enemyIns.transform.parent = transform;
                yield return new WaitForSeconds(0.5f);
            }
            
            currentWave++;

            if (transform.childCount <= 0)
            {
                wavesCleared = true;
                currentWave = 1;
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}


