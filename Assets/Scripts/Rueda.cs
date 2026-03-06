using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Rueda : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        TryKill(c.collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryKill(other);
    }

    void TryKill(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.TakeDamage(999);
        }
        else
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnPlayerDeath();
        }
    }
}