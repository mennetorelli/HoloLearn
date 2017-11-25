using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class PlacementController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.tag + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            other.gameObject.transform.position = gameObject.transform.position;

            other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;

            gameObject.SetActive(false);

            gameObject.GetComponentInParent<Counter>().Decrement();

        }
    }
}
