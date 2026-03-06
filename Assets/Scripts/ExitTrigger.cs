using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExitTrigger : MonoBehaviour
{
    void Reset()
    {
        Collider2D c = GetComponent<Collider2D>();
        if (c != null) c.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"ExitTrigger: OnTriggerEnter2D with {other.name} tag={other.tag}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("ExitTrigger: Player hit - calling GameManager.Win()");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Ganar();
                Debug.Log("ExitTrigger: GameManager.Win() called");
            }
            else Debug.LogWarning("ExitTrigger: GameManager.Instance is null");
        }
    }
}
