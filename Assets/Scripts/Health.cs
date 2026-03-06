using UnityEngine;

public class Health : MonoBehaviour
{
    //EL CODIGO ESTA HECHO EN UN PRINCIPIO PARA QUE TENGAS VARIAS VIDAS
    //PERO TRAS VARIOS PROBLEMAS Y DIFICULTADES EN EL DESRROLLO HE DECIDIDO
    //DEJARLO CON SOLO UNA VIDA, UN IMPACTO IMPLICA LA MUERTE
    public int maxHealth = 1;
    public int currentHealth;
    public bool destroyOnZero = true;

    Animator anim;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(currentHealth);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(currentHealth);

        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("[Health] DIE() EJECUTADO");
        if (anim) anim.SetBool("Dead", true);
        if (destroyOnZero) Destroy(gameObject, 0.5f);
    }
}