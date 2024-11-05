using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause ()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Freeze time
    }
    public void Home ()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1; // Go to main menu and don't freeze it
    }
    public void Resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Continue the game
    }
    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; // Initialize this scene
    }
}