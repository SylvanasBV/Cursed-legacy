using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    public GameObject canvasToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha tocado el trigger"); // Agrega esto para depurar
            if (canvasToActivate != null)
            {
                canvasToActivate.SetActive(true); // Activa el Canvas
                Debug.Log("Canvas activado");
            }
        }
    }
}