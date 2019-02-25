using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCollisionManager : MonoBehaviour
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
        if (other.tag != "Untagged")
        {
            List<string> tags = other.transform.GetComponent<TagsContainer>().tags;
            Debug.Log(tags);
            string weather = GameObject.Find("Weather").transform.GetChild(0).GetChild(0).tag;
            string temperature = GameObject.Find("Weather").transform.GetChild(0).GetChild(1).tag;

            foreach (string tag in tags)
            {
                Debug.Log(other.name + " tags possbili: " + tag);
                if (tags.Contains(weather) || tags.Contains(temperature))
                {
                    Counter.Instance.Decrement();

                    if (VirtualAssistantManager.Instance != null)
                    {
                        VirtualAssistantManager.Instance.Jump();
                    }

                    other.transform.GetComponent<ObjectPositionManager>().HasCollided(transform);
                }
            }

            if (VirtualAssistantManager.Instance != null && !VirtualAssistantManager.Instance.IsBusy)
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
        }
    }

}
