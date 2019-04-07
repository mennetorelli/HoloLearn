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
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            Counter.Instance.Decrement();

            transform.GetComponent<BinAudioManagerInterface>().PlayBinSound();

            if (VirtualAssistantManager.Instance != null)
            {
                VirtualAssistantManager.Instance.Jump();
            }

            other.transform.GetComponent<ObjectPositionManager>().HasCollided(transform);
        }
        else
        {
            GarbageCollectionManager manager = (GarbageCollectionManager)TaskManager.Instance;
            if (VirtualAssistantManager.Instance != null)
            {
                if (manager.activeBins.Contains(other.tag) && !VirtualAssistantManager.Instance.IsBusy)
                {
                    VirtualAssistantManager.Instance.ShakeHead();
                }
            }
        }
    }

}
