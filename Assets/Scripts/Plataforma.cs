using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public string requiredTag;
    public GameObject success;  
    public GameObject wrong;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            if (success != null)
            {
                success.SetActive(true); 
            }
        }
        else if (other.CompareTag("CuboAzul") || other.CompareTag("CuboAmarelo"))
        {
            if (wrong != null)
            {
                wrong.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            if (success != null)
            {
                success.SetActive(false);  
            }
        }
        else if (other.CompareTag("CuboAzul") || other.CompareTag("CuboAmarelo"))
        {
            if (wrong != null)
            {
                wrong.SetActive(false); 
            }
        }
    }
}


