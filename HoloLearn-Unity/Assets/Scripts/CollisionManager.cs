using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.tag + " " + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            other.transform.position = gameObject.transform.position;

            other.transform.rotation = transform.rotation;
            if (other.gameObject.tag == "Glass" || other.gameObject.tag == "Bottle")
            {
                other.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            }

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
