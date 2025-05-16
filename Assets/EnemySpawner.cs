using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;
    private Transform playerTransform;

    public void SetPlayer(GameObject player)
    {
        playerTransform = player.transform;
    }

    public void SpawnEnemy()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("No player assigned to spawner.");
            return;
        }

        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<HomingMover>().SetTarget(playerTransform);
    }
}
