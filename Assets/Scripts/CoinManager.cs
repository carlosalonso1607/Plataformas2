using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI CoinText;
    private int monedas = 0;

    private void Awake()
    {
        instance = this;
        ActualizarTexto();
    }

    public void SumarMoneda(int cantidad)
    {
        monedas += cantidad;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (CoinText != null) CoinText.text = monedas.ToString();
    }
}