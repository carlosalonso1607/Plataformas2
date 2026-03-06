using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muelle : MonoBehaviour
{
    public float FuerzaRebote;
    Animator animacion;
    bool rebotando = false;

    void Start()
    {
        animacion = GetComponent<Animator>();
    }

    void Update()
    {
        if (rebotando)
        {
            animacion.SetTrigger("Bota");
            rebotando = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rebotando = true;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {

                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * FuerzaRebote, ForceMode2D.Impulse);
                animacion.SetTrigger("Bota");
            }
        }
    }
}
