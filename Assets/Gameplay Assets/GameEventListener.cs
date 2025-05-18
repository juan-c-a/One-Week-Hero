using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        gameEvent.OnEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        gameEvent.OnEventRaised -= OnEventRaised;
    }

    private void OnEventRaised()
    {
        response.Invoke();
    }
}
