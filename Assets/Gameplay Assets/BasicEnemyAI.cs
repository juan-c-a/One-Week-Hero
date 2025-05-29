using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float stoppingDistance = 1.5f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Animator animator;

    private Transform target;
    private float lastAttackTime;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please tag the player object with 'Player'.");
        }
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectionRange)
        {
            // Mover hacia el jugador si está fuera del rango de parada
            if (distance > stoppingDistance)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                TryAttack();
            }
        }
        animator.SetFloat("Speed", transform.position.x);
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Debug.Log($"{gameObject.name} attacks the player!");
            // Aquí podrías aplicar daño al jugador o llamar a otro método.
        }
    }
}
