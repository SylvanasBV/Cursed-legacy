using UnityEngine;
public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que colisiona es el jugador
        {
            SceneController.Instance.LoadNextScene(); // Cargar la siguiente escena
        }
    }
}