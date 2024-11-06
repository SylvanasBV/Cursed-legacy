using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float detectionRadius = 5.0f;
     [SerializeField] float speed  = 2.0f;
    [SerializeField] int vidas;

     private Rigidbody2D rb;
     private Vector2 movement;
    private Animator animator;
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, direction.y); 
            animator.SetBool("First_Enemy_Run_Bool",true);
        }else
        {
            movement = Vector2.zero;
            animator.SetBool("First_Enemy_Run_Bool", false);
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);   
    }

    private void RemoveLive()
    {
        if (vidas > 0)
        {
            vidas--;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword") )
        {
            RemoveLive();
        } 
    }


}
