using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private List<Transform> spawnPositions;

    private int minimumEnemyCount = 1;
    private int maximumEnemyCount = 5;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int enemyCount = Random.Range(minimumEnemyCount, maximumEnemyCount + 1);
        for(int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex].transform.position;
    }
}
