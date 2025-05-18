using UnityEngine;

public class PlayerNotifier : MonoBehaviour
{
    [SerializeField] private GameEvent playerSpawnedEvent;

    private void Start()
    {
        playerSpawnedEvent.Raise();
    }
}
