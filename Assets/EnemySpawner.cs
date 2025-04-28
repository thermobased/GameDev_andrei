using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 3f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private bool spawnOnStart = true;

    [Header("Spawn Area")]
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(10f, 5f);
    [SerializeField] private bool visualizeSpawnArea = true;

    private int currentEnemyCount = 0;
    private Coroutine spawnRoutine;
    private Transform spawnAreaTransform;

    private void Start()
    {
        spawnAreaTransform = transform;
        
        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
            
        spawnRoutine = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            
            // Wait for random time between min and max interval
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnEnemy()
    {
        // Calculate a random position within the spawn area
        Vector2 randomOffset = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );
        
        Vector3 spawnPosition = spawnAreaTransform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
        
        // Instantiate enemy at random position
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        // Track enemy count
        currentEnemyCount++;
        
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }

    // Visualize spawn area in the editor
    private void OnDrawGizmos()
    {
        if (visualizeSpawnArea)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            Gizmos.DrawCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0.1f));
            
            // Draw outline
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0.1f));
        }
    }
}

