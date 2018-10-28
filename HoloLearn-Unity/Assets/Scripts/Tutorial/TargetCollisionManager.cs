using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class TargetCollisionManager : MonoBehaviour {

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
        //Debug.Log(gameObject.tag + " " + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;

            Counter.Instance.Decrement();

            if (VirtualAssistantManager.Instance != null)
            {
                VirtualAssistantManager.Instance.Jump();
            }
            
            if (other.gameObject.GetComponent<ObjectPositionManager>() != null)
            {
                other.gameObject.GetComponent<ObjectPositionManager>().HasCollided(transform);
            }
        }
        else
        {
            if (other.gameObject.tag != "Untagged" && !VirtualAssistantManager.Instance.IsBusy)
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
        }
    }
    
}
