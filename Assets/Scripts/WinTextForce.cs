using UnityEngine;
using UnityEngine.UI;

public class WinTextForce : MonoBehaviour
{
    public Text winText; 
   
    void Start()
    {
        if (winText != null)
        {
            winText.text = "¡HAS GANADO!";
            winText.color = Color.white;
            winText.gameObject.SetActive(true);
            winText.transform.SetAsLastSibling();
        }
    }
}
