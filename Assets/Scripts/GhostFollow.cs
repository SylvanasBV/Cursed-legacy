using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 2f;
    public Vector2 offsetRight = new Vector2(0.5f, 0f);
    public Vector2 offsetLeft = new Vector2(-0.5f, 0f);
    public float stopDistance = 0.5f; // Distancia mínima antes de empujar al jugador

    private bool isFacingRight = true;

    private void Update()
    {
        // Determina hacia dónde está mirando el jugador
        isFacingRight = player.transform.localScale.x > 0;

        // Ajusta el offset en función de la dirección
        Vector2 currentOffset = isFacingRight ? offsetRight : offsetLeft;
        Vector2 targetPosition = (Vector2)player.transform.position + currentOffset;

        // Verifica la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Solo sigue al jugador si está más lejos que stopDistance
        if (distanceToPlayer > stopDistance)
        {
            // Corrige la posición del acompañante utilizando Lerp
            transform.position = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            // Si está muy cerca, ajusta la posición sin sobreponerse
            Vector2 newPosition = targetPosition;
            newPosition.x = Mathf.Clamp(newPosition.x, player.transform.position.x - stopDistance, player.transform.position.x + stopDistance);
            transform.position = newPosition;
        }

        // Ajusta la escala del acompañante para que coincida con la dirección del jugador
        transform.localScale = new Vector3(isFacingRight ? 1f : -1f, 1f, 1f); // Mirando a la derecha o izquierda

        // Opcional: Visualiza la posición objetivo
        Debug.DrawLine(player.transform.position, targetPosition, Color.red);
    }
}
