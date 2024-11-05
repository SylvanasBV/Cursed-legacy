using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantener el objeto en cada escena
    }
    // Cargar la escena con transición por nombre
    public void LoadScene(string sceneName)
    {
        SceneTransition.Instance.LoadSceneWithTransition(sceneName);
    }
    // Cargar la siguiente escena en el orden de Build Settings
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        // Volver al menú si estamos en el último nivel (Level3)
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            LoadScene("Menu");
        }
        else
        {
            SceneTransition.Instance.LoadSceneWithTransition(SceneManager.GetSceneByBuildIndex(nextSceneIndex).name);
        }
    }
    // Volver al menú desde cualquier nivel
    public void LoadMenu()
    {
        SceneTransition.Instance.LoadSceneWithTransition("Menu");
    }
    // Cargar el primer nivel desde el menú
    public void LoadLevel1()
    {
        if (SceneTransition.Instance != null)
        {
            SceneTransition.Instance.LoadSceneWithTransition("Map01"); // Asegúrate de que el nombre coincide
        }
        else
        {
            Debug.LogError("SceneTransition.Instance es null");
        }
    }
}