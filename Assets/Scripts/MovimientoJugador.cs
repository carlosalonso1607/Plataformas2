using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class PlayerController : MonoBehaviour
{
    [Header("Estadísticas de Movimiento")]
    public float velocidadCaminar = 5f;
    public float velocidadCorrer = 10f;
    public float fuerzaSalto = 10f;
    private float velocidadActual;

    [Header("Detección de Suelo")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 groundBox = new Vector2(0.4f, 0.1f);
    
    [Header("Mejoras de Salto (Game Feel)")]
    public float coyoteTime = 0.15f; 
    private float coyoteTimeCounter;
    public float jumpBufferTime = 0.15f; 
    private float jumpBufferCounter;

    [Header("Saltos y Audio")]
    public int saltosMaximos = 1; 
    private int saltosRestantes;
    public AudioSource audioSalto;

    [Header("Componentes")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Health health;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        if (groundCheck == null)
        {
            Transform t = transform.Find("groundCheck");
            if (t != null) groundCheck = t;
        }
    }

    void Update()
    {
        if (isDead) return;

        float movimiento = Input.GetAxis("Horizontal");
        velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidadCaminar;
        rb.velocity = new Vector2(movimiento * velocidadActual, rb.velocity.y);

        if (movimiento > 0.1f) SetFacing(true);
        else if (movimiento < -0.1f) SetFacing(false);

        bool enSuelo = IsGrounded();

        if (enSuelo) 
        {
            coyoteTimeCounter = coyoteTime;
            saltosRestantes = saltosMaximos;
        }
        else 
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f || saltosRestantes > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); 
                rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

                jumpBufferCounter = 0f;
                
                if (coyoteTimeCounter <= 0f) saltosRestantes--;
                else coyoteTimeCounter = 0f;

                if (audioSalto != null) audioSalto.Play();
            }
        }

        if (animator != null)
        {
            animator.SetBool("isRunning", Mathf.Abs(movimiento) > 0.1f);
            if (HasParameter(animator, "Grounded")) animator.SetBool("Grounded", enSuelo);
            if (HasParameter(animator, "yVelocity")) animator.SetFloat("yVelocity", rb.velocity.y);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;
        
        health.TakeDamage(dmg);

        if (health.currentHealth <= 0) Die();
    }

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero; 
        if (animator != null && HasParameter(animator, "Dead")) animator.SetBool("Dead", true);

        if (GameManager.Instance != null) GameManager.Instance.OnPlayerDeath();
        else Debug.LogError("¡CUIDADO! GameManager no encontrado.");

        this.enabled = false;
    }

    bool IsGrounded()
    {
        Vector2 origen = groundCheck != null ? (Vector2)groundCheck.position : (Vector2)transform.position + Vector2.down * 0.5f;
        Collider2D hit = Physics2D.OverlapBox(origen, groundBox, 0f, groundLayer);
        return hit != null;
    }

    void SetFacing(bool faceRight)
    {
        facingRight = faceRight;
        if (spriteRenderer != null) spriteRenderer.flipX = !faceRight;
    }

    bool HasParameter(Animator a, string paramName)
    {
        foreach (var p in a.parameters) if (p.name == paramName) return true;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Vector2 origen = groundCheck != null ? (Vector2)groundCheck.position : (Vector2)transform.position + Vector2.down * 0.5f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origen, groundBox);
    }
}