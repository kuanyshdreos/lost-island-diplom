using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    public GameObject bombPrefab;
    public List<Transform> spawnPoints;
    public GameObject player;

    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 4f;
    public float playerSpawnRadius = 3f;
    public float spawnNearPlayerProbability = 0.5f;
    public float bombLifetime = 5f; // Длительность жизни бомбы

    void Start()
    {
        StartCoroutine(SpawnBombsWithDelay());
    }

    IEnumerator SpawnBombsWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        bool spawnNearPlayer = Random.value < spawnNearPlayerProbability;
        if (spawnNearPlayer)
        {
            Vector3 spawnPosition = GetRandomSpawnPositionNearPlayer();

            if (spawnPosition != Vector3.zero)
            {
                GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
                Destroy(bomb, bombLifetime);
            }
            else
            {
                Debug.LogWarning("Не удалось найти позицию для спавна бомбы рядом с игроком.");
            }
        }
        else
        {
            Transform spawnPoint = GetRandomSpawnPoint();

            if (spawnPoint != null)
            {
                GameObject bomb = Instantiate(bombPrefab, spawnPoint.position, Quaternion.identity);
                Destroy(bomb, bombLifetime);
            }
            else
            {
                Debug.LogWarning("Не удалось найти позицию для спавна бомбы.");
            }
        }
    }

    Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }

    Vector3 GetRandomSpawnPositionNearPlayer()
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * playerSpawnRadius;
        Vector3 randomSpawnOffset = new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y);
        Vector3 spawnPosition = player.transform.position + randomSpawnOffset;

        return spawnPosition;
    }
}