using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform player;   // El transform del jugador
    public float zOffset = -10f;  // La distancia fija en Z (usualmente -10 para cámara 2D en Unity)
    void LateUpdate()
    {
        // Establece la posición de la cámara en la X y Y del jugador, manteniendo el Z fijo
        transform.position = new Vector3(player.position.x, player.position.y, zOffset);
    }
}