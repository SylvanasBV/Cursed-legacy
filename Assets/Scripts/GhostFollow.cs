using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostFollow : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float followDistance = 1.0f; //la distancia a la que se detiene del jugador
    public  Vector2 offsetRight = new Vector2(0.5f, 0.5f); //offset respecto al jugador
    public  Vector2 offsetLeft = new Vector2(-0.5f, 0.5f);

    
    private PlayerController playerController;
    private Vector2 currentOffset;
    // Start is called before the first frame update

    // First call of the Ghost
    public bool StartPosition;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        currentOffset = offsetRight; // Iniciar con el offset a la derecha
        StartPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Actualizar el offset según la dirección del jugador
        if (playerController.isFacingRight)
        {
            currentOffset = offsetRight;
        }
        else
        {
            currentOffset = offsetLeft;
        }

        Vector2 targetPosition = (Vector2)player.transform.position + currentOffset;
        float distance = Vector2.Distance(transform.position, targetPosition);

        // Mover al acompañante solo si está lejos del punto fijado
        if (distance > followDistance && StartPosition)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
