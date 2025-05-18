using UnityEngine;

public class SpawnerWavesPRO : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float difficultyMultiplier = 1.1f;

    private bool isSpawning = false;
    private float waveTimer = 0f;
    private float spawnTimer = 0f;
    private int enemiesSpawnedThisWave = 0;

    private void Update()
    {
        if (!isSpawning || enemySpawner == null) return;

        waveTimer += Time.deltaTime;

        if (waveTimer >= timeBetweenWaves && enemiesSpawnedThisWave <= 0)
        {
            StartNewWave();
        }

        if (enemiesSpawnedThisWave > 0)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                enemySpawner.SpawnEnemy();
                enemiesSpawnedThisWave--;
            }
        }
    }

    private void StartNewWave()
    {
        waveTimer = 0f;
        spawnTimer = 0f;
        enemiesSpawnedThisWave = enemiesPerWave;

        Debug.Log($"Starting new wave with {enemiesSpawnedThisWave} enemies.");

        // Increase difficulty for next wave
        enemiesPerWave = Mathf.CeilToInt(enemiesPerWave * difficultyMultiplier);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        waveTimer = timeBetweenWaves; // Trigger first wave immediately
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
