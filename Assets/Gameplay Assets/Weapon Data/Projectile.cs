using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private Vector2 direction;
    private float speed;
    private float lifetime;

    public void Initialize(Vector2 dir, int dmg, float spd, float life)
    {
        direction = dir.normalized;
        damage = dmg;
        speed = spd;
        lifetime = life;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}