using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Pinchos : MonoBehaviour
{
    [Header("Daño")]
    public int damage = 1;
    public float hitCooldown = 1.2f; 
    [Header("Knockback")]
    public float fuerzaArriba = 8f;   
    public float fuerzaLateral = 2f;  

    float lastHitTime = -999f;

    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryHit(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        TryHit(other);
    }

    void TryHit(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - lastHitTime < hitCooldown) return;

        lastHitTime = Time.time;

        var pc = other.GetComponent<PlayerController>();
        if (pc != null) pc.TakeDamage(damage);

        Rigidbody2D prb = other.attachedRigidbody;
        if (prb != null)
        {
            float dir = Mathf.Sign(other.transform.position.x - transform.position.x);
            if (dir == 0) dir = 1f;
            prb.velocity = Vector2.zero;
            prb.AddForce(new Vector2(dir * fuerzaLateral, fuerzaArriba), ForceMode2D.Impulse);
        }
    }
}