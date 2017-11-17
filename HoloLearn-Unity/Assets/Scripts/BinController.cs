using UnityEngine;

public class CanController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag(other.tag))
        {
            other.gameObject.SetActive(false);
        }        
    } 
}
