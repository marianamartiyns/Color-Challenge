using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameObject carryObject;
    private bool gameHasEnded = false;
    public float restartDelay = 1f;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Métodos para acessar e modificar o objeto que está sendo carregado
    public static GameObject GetCarryObject()
    {
        return carryObject;
    }

    public static void SetCarryObject(GameObject obj)
    {
        carryObject = obj;
        if (obj != null)
        {
            DontDestroyOnLoad(obj);
        }
    }

    public void RestartLevel()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Reiniciando a cena...");
            Invoke("ReloadCurrentScene", restartDelay);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}





