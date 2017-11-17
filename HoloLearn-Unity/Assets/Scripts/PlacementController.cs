using UnityEngine;

public class PlacementController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.tag + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            Vector3 newPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            other.gameObject.transform.position = newPos;

            gameObject.SetActive(false);
        }
    }
}
