using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Rigidbody2D cuerpo;
    private Transform objetivo;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
    }


}