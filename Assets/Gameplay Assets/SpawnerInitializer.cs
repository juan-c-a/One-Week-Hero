using UnityEngine;

public class SpawnerInitializer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject player;

    private void Start()
    {
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner no asignado en el inspector.");
            return;
        }

        if (player == null)
        {
            // Intenta encontrar al jugador por etiqueta
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("No se encontró un objeto con el tag 'Player'. Por favor, asigna el jugador manualmente.");
                return;
            }
        }

        enemySpawner.SetPlayer(player);
        Debug.Log("Player asignado correctamente al EnemySpawner.");
    }
}
