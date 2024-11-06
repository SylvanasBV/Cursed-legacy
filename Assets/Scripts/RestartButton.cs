using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para gestionar las escenas
using UnityEngine.UI; // Para interactuar con los UI elementos


public class RestartButton : MonoBehaviour
{
    // Referencia al botón de reinicio
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        // Al principio desactivamos el botón de reinicio
        if (restartButton != null)
        {
            restartButton.SetActive(false); // Desactiva el botón inicialmente
        }
    }

    // Método que se llama para reiniciar el juego
    public void Restart()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        // Si quieres restaurar el tiempo (en caso de que se haya detenido al morir)
        Time.timeScale = 1f; // Vuelve a activar el tiempo en la escena
    }

    // Método que activa el botón de reinicio
    public void ShowRestartButton()
    {
        if (restartButton != null)
        {
            restartButton.SetActive(true); // Activa el botón cuando sea necesario
        }
    }
}