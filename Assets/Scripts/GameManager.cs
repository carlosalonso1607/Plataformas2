using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject ganar;
    public GameObject perder;

    [Header("Settings")]
    public bool pauseOnEnd = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        
        Instance = this;

        Time.timeScale = 1f;

        if (ganar) ganar.SetActive(false);
        if (perder) perder.SetActive(false);
    }

    public void Ganar()
    {
        if (ganar != null) ActivatePanel(ganar);
        else Debug.LogError("Panel de GANAR no asignado en el GameManager");
    }

    public void Perder()
    {
        if (perder != null) ActivatePanel(perder);
        else Debug.LogError("Panel de PERDER no asignado en el GameManager");
    }

    public void ActivatePanel(GameObject panel)
    {
        if (panel == null) return;

        if (pauseOnEnd) Time.timeScale = 0f;

        panel.SetActive(true);

        Canvas canvas = panel.GetComponentInParent<Canvas>();
        if (canvas != null) canvas.sortingOrder = 1000; 

        panel.transform.SetAsLastSibling();

        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        RectTransform rt = panel.GetComponent<RectTransform>();
        if (rt != null && rt.localScale == Vector3.zero) rt.localScale = Vector3.one;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPlayerDeath()
    {
        Perder();
    }
}