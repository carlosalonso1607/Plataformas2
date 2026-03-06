using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class MovimientoCamara : MonoBehaviour
{
    public GameObject personaje;
    public GameObject fondo;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 posJugador = personaje.transform.position;
        Vector3 posCamara  = transform.position;
        float vInicial = 0.0f;
        float x = Mathf.SmoothDamp(posCamara.x, posJugador.x,
            ref vInicial, 0.1f);
        posCamara.x = x;
        transform.position = posCamara;
    }
}
