using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class PlacementController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.tag + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            other.gameObject.transform.position = gameObject.transform.position;

            other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;
            //other.gameObject.GetComponent<CustomHandDraggable>().isDragging = false;

            gameObject.SetActive(false);

            Counter.Instance.Decrement();

        }
    }
}
