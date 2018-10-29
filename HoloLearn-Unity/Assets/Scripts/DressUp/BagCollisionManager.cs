﻿using HoloToolkit.Unity.InputModule;
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
        DressUpManager manager = (DressUpManager)TaskManager.Instance;

        List<string> tags = new List<string>();
        if (other.tag != null)
        {
            Transform objectTags = other.transform.GetChild(other.transform.childCount - 1);
            foreach (Transform objectTag in objectTags)
            {
                tags.Add(objectTag.tag);
            }
        }

        foreach (string tag in tags)
        {
            Debug.Log(tag);
            if (manager.activeWeatherTags.Contains(tag))
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
                if (!VirtualAssistantManager.Instance.IsBusy)
                {
                    VirtualAssistantManager.Instance.ShakeHead();
                }
            }
        }
    }

}
