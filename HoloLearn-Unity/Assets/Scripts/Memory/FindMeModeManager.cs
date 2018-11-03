﻿using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindMeModeManager : PlayModeManager
{
    public Transform selectedElement;
    public Transform objectToFind;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void HandleTap(Transform selectedElement)
    {
        selectedElement.GetChild(0).gameObject.SetActive(false);
        selectedElement.GetChild(1).gameObject.SetActive(true);

        Transform manager = GameObject.Find("MemoryManager").transform.GetChild(0);
        manager.GetComponent<FindMeModeManager>().selectedElement = selectedElement;

        manager.GetChild(1).gameObject.SetActive(true);
    }


    public override List<Transform> GenerateObjects(GameObject ObjectsPrefabs, int numberOfBoxes)
    {
        System.Random rnd = new System.Random();

        List<Transform> objs = new List<Transform>();
        for (int i = 1; i <= numberOfBoxes; i++)
        {
            int j = rnd.Next(0, ObjectsPrefabs.transform.childCount);
            Transform obj = ObjectsPrefabs.transform.GetChild(j);
            objs.Add(obj);
        }

        Counter.Instance.InitializeCounter(1);

        return objs;
    }


    public override void StartGame(int waitingTime)
    {
        StartCoroutine(ShowObjectToFind(waitingTime));
    }

    private IEnumerator ShowObjectToFind(int waitingTime)
    {
        System.Random rnd = new System.Random();

        Transform elems = GameObject.Find("Elements").transform;
        objectToFind = elems.GetChild(rnd.Next(0, elems.childCount)).GetChild(1);
        Instantiate(objectToFind, transform.GetChild(0).position + new Vector3(0f, -0.2f, 0f), transform.GetChild(0).rotation, transform.GetChild(0));
        transform.GetChild(0).gameObject.SetActive(true);
  
        yield return new WaitForSeconds(waitingTime);

        transform.GetChild(0).gameObject.SetActive(false);
    }


    public void YesConfirmation()
    {
        transform.GetChild(1).gameObject.SetActive(false);

        if (selectedElement.gameObject.name == objectToFind.gameObject.name)
        {
            VirtualAssistantManager.Instance.Jump();
            Counter.Instance.Decrement();
        }
        else
        {
            VirtualAssistantManager.Instance.ShakeHead();
            selectedElement.GetChild(0).gameObject.SetActive(true);
            selectedElement.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void NoConfirmation()
    {
        transform.GetChild(1).gameObject.SetActive(false);

        selectedElement.GetChild(0).gameObject.SetActive(true);
        selectedElement.GetChild(1).gameObject.SetActive(false);

        selectedElement = null;
    }

}
