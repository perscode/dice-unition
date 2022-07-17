using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float xPos;
    private float yPos;
    private int enemyCount;
    public int maxEnemyCount = 250;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCount < maxEnemyCount)
        {
            Vector3 enemySpawnPoint = Camera.main.ViewportToWorldPoint(getVectorOutsideView());
            Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            enemyCount += 1;
        }

    }

    Vector3 getVectorOutsideView() 
    {
        int side = Random.Range(1, 5);

        switch (side)
        {
            case 1: xPos = -0.1f; yPos = Random.value;  break;
            case 2: xPos = Random.value; yPos = 1.1f; break;
            case 3: xPos = 1.1f; yPos = Random.value; break;
            case 4: xPos = Random.value; yPos = -0.1f; break;
        }
        return new Vector3(xPos, yPos, 0);
    }
}
