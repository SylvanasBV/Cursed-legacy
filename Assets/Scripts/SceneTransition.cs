using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }
    [SerializeField] private Image transitionImage; // La imagen que se usará para la transición
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // Imagen no esté visible al principio
        transitionImage.gameObject.SetActive(false); 
    }
    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }
    private IEnumerator TransitionToScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("El nombre de la escena está vacío.");
            yield break; // Sale del coroutine si el nombre de la escena es inválido
        }
        transitionImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        SceneManager.LoadScene(sceneName); // Intenta cargar la escena
    }
}