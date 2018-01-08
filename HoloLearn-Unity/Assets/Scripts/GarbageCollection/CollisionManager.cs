using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GarbageCollection
{
    public class CollisionManager : MonoBehaviour
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

                VirtualAssistantManager.Instance.Jump();

                Counter.Instance.Decrement();

                Destroy(other);
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
}
