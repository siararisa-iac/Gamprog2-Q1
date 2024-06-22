using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private List<Transform> spawnPositions;

    [SerializeField] private int minimumEnemyCount = 1;
    [SerializeField] private int maximumEnemyCount = 5;

    [SerializeField] private float minSpawnTime = 3.0f;
    [SerializeField] private float maxSpawnTime = 5.0f;

    private int enemyCount = 0;
    private float spawnTime = 0;

    private void Start()
    {
        // Call coroutines with the StartCoroutine()
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemies();
        //After finishing this call, call it again to do the loop
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SampleCoroutine()
    {
        Debug.Log("Starting to do something");
        //yield return new WaitForSeconds(5.0f);
        float timeElapsed = 0;
        while (timeElapsed < 5.0f)
        {
            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);
            // This will wait for the next frame before proceeding
            yield return null;
        }
        Debug.Log("Wait has finished");
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
