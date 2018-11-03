using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindMeModeManager : PlayModeManager
{
    
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update () {
		
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
        Transform elem = elems.GetChild(rnd.Next(0, elems.childCount));

        MemoryManager manager = (MemoryManager)TaskManager.Instance;
        manager.selectedElement = elem.gameObject;

        Transform objectToFind = elem.GetChild(1);
        Instantiate(objectToFind, transform.GetChild(0).position + new Vector3(0f, -0.2f, 0f), transform.GetChild(0).rotation, transform.GetChild(0));
        transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(waitingTime);

        transform.GetChild(0).gameObject.SetActive(false);
    }

}
