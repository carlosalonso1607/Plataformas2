using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float arrivalDistance = 0.05f;
    public SpriteRenderer spriteRenderer;

    [Header("Daño")]
    public float hitCooldown = 1f;
    float lastHitTime = -999f;

    Rigidbody2D rb;
    Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB;
    }

    void FixedUpdate()
    {
        float newX = Mathf.MoveTowards(rb.position.x, target.position.x, speed * Time.fixedDeltaTime);
        rb.MovePosition(new Vector2(newX, rb.position.y));

        if (Mathf.Abs(newX - target.position.x) <= arrivalDistance)
            target = (target == pointA) ? pointB : pointA;
    }

    void OnCollisionEnter2D(Collision2D c) => TryDamagePlayer(c.collider);

    void TryDamagePlayer(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time - lastHitTime > hitCooldown)
        {
            lastHitTime = Time.time;
            other.GetComponent<PlayerController>()?.TakeDamage(1);
        }
    }
}