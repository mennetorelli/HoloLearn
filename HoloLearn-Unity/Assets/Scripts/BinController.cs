using UnityEngine;

public class BinController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag(other.tag))
        {
            other.gameObject.SetActive(false);
        }        
    } 
}
