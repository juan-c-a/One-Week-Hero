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
        if (!isSpawning) return;

        // Wave countdown
        waveTimer += Time.deltaTime;

        if (waveTimer >= timeBetweenWaves)
        {
            // Start spawning current wave
            SpawnWave();
        }

        // Spawn enemies in current wave
        if (enemiesSpawnedThisWave > 0)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                enemySpawner.SpawnEnemy();
                enemiesSpawnedThisWave--;

                // Debug.Log($"Enemy spawned. Remaining in wave: {enemiesSpawnedThisWave}");
            }
        }
    }

    private void SpawnWave()
    {
        waveTimer = 0f;
        spawnTimer = 0f;
        enemiesSpawnedThisWave = enemiesPerWave;

        // Increment difficulty
        enemiesPerWave = Mathf.CeilToInt(enemiesPerWave * difficultyMultiplier);

        Debug.Log($"Starting new wave with {enemiesSpawnedThisWave} enemies.");
    }

    public void StartSpawning()
    {
        isSpawning = true;
        waveTimer = timeBetweenWaves; // So first wave spawns immediately
    }
}
