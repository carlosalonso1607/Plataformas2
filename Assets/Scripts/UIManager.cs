using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Tooltip("Texto que muestra la vida del jugador")]
    public TMP_Text healthText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (healthText != null) healthText.text = "Vida: 5";
    }

    public void UpdateHealth(int val)
    {
        if (healthText)
        {
            healthText.text = val > 0 ? "Vida: " + val : "Vida: 0";
        }
    }
}