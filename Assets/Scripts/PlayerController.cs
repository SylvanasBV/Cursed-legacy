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

    //Referencia texto muerte
    public GameObject deathMessage;


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
        }

        FlipSprite();
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
            for (int i = 0; i <= RelicLevel.Length; i++)
                if (RelicLevel != null && collision.gameObject.CompareTag("Relic") == RelicLevel[i])
                {
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
