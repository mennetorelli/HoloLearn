using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class BinCollisionManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.tag + " " + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;

            Counter.Instance.Decrement();

            if (VirtualAssistantManager.Instance != null)
            {
                VirtualAssistantManager.Instance.Jump();
            }

            other.gameObject.SetActive(false);
        }
        else
        {
            GarbageCollectionManager manager = (GarbageCollectionManager)TaskManager.Instance;
            if (manager.activeBins.Contains(other.tag))
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
        }
    }

}
