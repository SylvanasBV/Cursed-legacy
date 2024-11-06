using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Nombre de la escena que quieres cargar

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}