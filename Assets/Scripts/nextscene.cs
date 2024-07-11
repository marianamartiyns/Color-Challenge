using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject carryObject = GameManager.GetCarryObject();
            if (carryObject != null)
            {
                // tenta remover o objeto do seu parent temporariamente 
                carryObject.transform.parent = null;
                DontDestroyOnLoad(carryObject);
            }

            // Carrega a próxima cena
            SceneManager.LoadScene(sceneToLoad);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reposiciona o objeto carregado
        GameObject carryObject = GameManager.GetCarryObject();
        if (carryObject != null)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            carryObject.transform.SetParent(playerTransform.Find("HoldObject"));
            carryObject.transform.localPosition = Vector3.zero;
            carryObject.transform.localRotation = Quaternion.identity;
        }

        // Remove a inscrição do evento após ter sido chamado
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}








