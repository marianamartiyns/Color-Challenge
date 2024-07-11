using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject = null;
    public GameObject PickUpText;
    public GameObject InstructionsText;
    public float instructionsDisplayTime = 5f;
    private bool isNearPickableObject = false;

    private void Start()
    {
        InstructionsText.SetActive(true);
        StartCoroutine(HideInstructionsTextAfterDelay());
    }

    private IEnumerator HideInstructionsTextAfterDelay()
    {
        yield return new WaitForSeconds(instructionsDisplayTime);
        InstructionsText.SetActive(false);
    }

    // Interação com o Objeto
    private void Update()
    {
        CheckForPickableObjects();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpObject();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DropObject();
        }
    }

    private void CheckForPickableObjects()
    {
        if (heldObject == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            isNearPickableObject = false;
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("CuboAzul") || collider.CompareTag("CuboAmarelo"))
                {
                    PickUpText.SetActive(true);
                    isNearPickableObject = true;
                    break;
                }
            }

            if (!isNearPickableObject)
            {
                PickUpText.SetActive(false);
            }
        }
    }

    private void TryPickUpObject()
    {
        if (heldObject == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("CuboAzul") || collider.CompareTag("CuboAmarelo"))
                {
                    PickUpObject(collider.gameObject);
                    PickUpText.SetActive(false);
                    break;
                }
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        obj.SetActive(true);
        obj.transform.SetParent(holdPoint);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.GetComponent<Rigidbody>().isKinematic = true;

        GameManager.SetCarryObject(obj);
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);
            heldObject = null;

            GameManager.SetCarryObject(null);
        }
    }

    // Interação com paredes para colisão e reiniciar cena
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Parede"))
        {
            RestartScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parede"))
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
