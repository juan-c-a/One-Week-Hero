using UnityEngine;

public class HomingMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Transform targetTransform;

    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }

    private void Update()
    {
        if (targetTransform == null) return;

        Vector3 direction = (targetTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
