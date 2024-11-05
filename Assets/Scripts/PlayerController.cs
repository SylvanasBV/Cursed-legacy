using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movimiento y velocidad
    public float horizontalInput;
    public float verticalInput;
    private float moveSpeed = 5f;
    public bool isFacingRight = true;

    public Rigidbody2D rb;
    Animator animator;
    GameObject sword;

    //Referencia texto muerte
    public GameObject deathMessage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sword = GameObject.Find("Weapon");
    }
    // Update is called once per frame
    void Update()
    {
         //Movimiento jugador
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        FlipSprite();
    }

    //Collision con el enmigo 
    private void FixedUpdate()
    {
        // Verificar si colisiona con un objeto que tenga el tag "Enemy"
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            Vector3 lsWeapon = sword.transform.localScale;

            ls.x *= -1f;
            lsWeapon.y -= 1f;

            transform.localScale = ls;
            sword.transform.localScale = new Vector3 (1,lsWeapon.y,0);
        }
    }
}
