using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 5f;
    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Autodestrucción al pasar lifetime
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void Initialize(Vector2 newDirection, int newDamage)
    {
        direction = newDirection.normalized;
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignorar colisión con quien lo disparó (opcional: layer mask o tag check)
        if (collision.gameObject == this.gameObject)
            return;

        // Verificar si el objeto golpeado es damageable
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        // Efecto de impacto, partículas, sonido, etc. (opcional aquí)
        Debug.Log($"Projectile hit {collision.gameObject.name}");

        Destroy(gameObject); // Destruir proyectil al impactar
    }
}
