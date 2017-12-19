using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.tag + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            other.gameObject.transform.position = gameObject.transform.position;
            other.gameObject.transform.rotation = gameObject.transform.rotation;

            other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;

            Destroy(gameObject);

            VirtualAssistantManager.Instance.Jump();

            Counter.Instance.Decrement();

        }
        else
        {
            if (other.gameObject.tag != "VirtualAssistant")
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
        }
    }
}
