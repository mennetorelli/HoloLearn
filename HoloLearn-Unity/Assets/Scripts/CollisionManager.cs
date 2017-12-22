using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

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
        
        //this.other = other.transform;
        //StartCoroutine(AdjustPosition());

        Debug.Log(gameObject.tag + " " + other.tag);
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;
            
            VirtualAssistantManager.Instance.Jump();

            Counter.Instance.Decrement();

            other.gameObject.GetComponent<ObjectPositionManager>().AdjustTransform(transform);
            //StartCoroutine(AdjustPosition(other));

            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        else
        {
            if (other.gameObject.tag != "VirtualAssistant")
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
        }
    }

    private IEnumerator AdjustPosition(Collider other)
    {
        float lerpPercentage = 0f;
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.rotation;

        while (lerpPercentage < 1)
        {
            lerpPercentage += Time.deltaTime * 0.01f;
            other.transform.position = Vector3.Lerp(position, transform.position, lerpPercentage);
            other.transform.rotation = Quaternion.Lerp(rotation, transform.rotation, lerpPercentage);
            Debug.Log(lerpPercentage);
            yield return null;
        }
    }

}
