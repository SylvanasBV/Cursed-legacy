using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    //movimiento y velocidad
    public float horizontalInput;
    public float verticalInput;
    private float moveSpeed = 5f;
    public bool isFacingRight = true;

    public Rigidbody2D rb;

    Animator animator;
    Animator swordAnimator;
    GameObject sword;
    public GameObject[] RelicLevel;
    ParticleSystem playerParticleAttack;
    public Collider2D rangoCollider;


    public HealthBar healthBar; // Referencia al script de la barra de salud
    public GameObject deathMessage; // Referencia al mensaje de muerte (imagen en el Canvas)

    private float currentHealth; // Salud actual del jugador
    public float maxHealth = 100f; // Salud máxima del jugador
    public GameObject restartButton; // Declarar la referencia al botón

    GhostFollow ghostFollow;
    GameObject firstEnemy;
    // Start is called before the first frame update
    void Start()
    {
        ghostFollow = FindObjectOfType<GhostFollow>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sword = GameObject.Find("Weapon");
        swordAnimator = sword.GetComponent<Animator>();
        sword.SetActive(false);

        // Intentamos asignar el ParticleSystem desde "Old_Weapon"
        GameObject weapon = GameObject.Find("OldPlayer/SworsPack/Weapon/Old_Weapon");
        if (weapon != null)
        {
            playerParticleAttack = weapon.transform.Find("Particle System")?.GetComponent<ParticleSystem>();
            if (playerParticleAttack == null)
                Debug.LogWarning("No se encontró el Particle System en Old_Weapon.");
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto Old_Weapon.");
        }

        // Inicializamos la salud del jugador
        currentHealth = maxHealth;
        Debug.Log("Salud inicial: " + currentHealth);

        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
            if (healthBar == null)
            {
                Debug.LogError("No se encontró el script HealthBar. Asígnenlo en el Inspector.");
            }
        }

        if (deathMessage != null)
        {
            deathMessage.SetActive(false);  // Asegúrate de que el mensaje de muerte esté desactivado al inicio
        }
        else
        {
            Debug.LogError("El mensaje de muerte no está asignado. Asígnenlo en el Inspector.");
        }

    }
    // Update is called once per frame
    void Update()
    {
         //Movimiento jugador
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            swordAnimator.SetTrigger("SwordAttack1Trigger");
            // Verificar si playerParticleAttack está asignado y luego reproducirlo
            if (playerParticleAttack != null)
            {
                Debug.Log("Ejecutando ParticleSystem.");
                playerParticleAttack.Play();
                rangoCollider.enabled = !rangoCollider.enabled;
            }
            else
            {
                Debug.LogWarning("No se encontró el ParticleSystem para ejecutar.");
            }
        }

        FlipSprite();

        // Verifica si la salud es 0 o menor
        Debug.Log("Salud actual: " + currentHealth); // Verificamos el valor de la salud en cada frame
        if (currentHealth <= 0)
        {
            Debug.Log("Jugador ha muerto");
            Die(); // Si la salud es 0 o menos, se llama al método Die()
        }
    }

    //Collision con el enmigo 
    private void FixedUpdate()
    {
        // Verificar si colisiona con un objeto que tenga el tag "Enemy"
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
        if (horizontalInput != 0 && verticalInput == 0)
        {
            animator.SetFloat("Run_Float", Math.Abs(horizontalInput));
        }
        else
        {
            animator.SetFloat("Run_Float", Math.Abs(verticalInput));
        }
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            ghostFollow.StartPosition = true;
            swordAnimator.SetBool("SwordPrepairAttack", true);
        }
        if (collision.CompareTag("SwordFloor"))
        {
            Destroy(collision.gameObject);
            sword.SetActive(true);
        }

        if (collision.CompareTag("Relic"))
        {
            for (int i = 0; i < RelicLevel.Length; i++) // Cambiado a < RelicLevel.Length
            {
                if (RelicLevel[i] != null && collision.gameObject == RelicLevel[i]) // Verifica que sea el mismo GameObject
                {
                    Debug.Log("RelicsDestroy");
                    RelicLevel[i].GetComponent<Animator>().SetBool("Destroy_Hex_Bool", true);
                    RelicLevel[i].GetComponentInChildren<ParticleSystem>().Stop();
                }
                else
                {
                    Debug.Log("No encontrado");
                }
            }
        }
    }

        void Die()
    {
        // Desactivar al jugador
        gameObject.SetActive(false);

        // Mostrar el mensaje de muerte
        if (deathMessage != null)
        {
            deathMessage.SetActive(true);
        }

        // Mostrar el botón de reinicio
        if (restartButton != null)
        {
            restartButton.SetActive(true); // Activa el botón de reinicio
        }

        // Detener el juego (opcional)
        Time.timeScale = 0f; // Detiene el tiempo (el juego se pausa)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colisión con enemigo detectada.");
            TakeDamage(10f); // Llama a TakeDamage cuando se colisiona con un enemigo
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Salud actual: " + currentHealth);

        // Verifica si la salud llega a 0
        if (currentHealth <= 0)
        {
            Die();  // Llama a la función que maneja la muerte
        }
    }    
}
