using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassicModeManager : PlayModeManager
{

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override List<Transform> GenerateObjects(GameObject ObjectsPrefabs, int numberOfBoxes)
    {
        System.Random rnd = new System.Random();

        List<Transform> objs = new List<Transform>();
        for (int i = 1; i <= numberOfBoxes / 2; i++)
        {
            int j = rnd.Next(0, ObjectsPrefabs.transform.childCount);
            Transform obj = ObjectsPrefabs.transform.GetChild(j);
            objs.Add(obj);
            objs.Add(obj);
        }

        Counter.Instance.InitializeCounter(objs.Count);

        return objs;
    }

    public override void StartGame(int waitingTime)
    {
        StartCoroutine(ShowObjects(waitingTime));
    }

    private IEnumerator ShowObjects(int waitingTime)
    {
        Transform elems = GameObject.Find("Elements").transform;

        for (int i = 0; i < elems.childCount; i++)
        {
            elems.GetChild(i).GetChild(0).gameObject.SetActive(false);
            elems.GetChild(i).GetChild(1).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(waitingTime);

        for (int i = 0; i < elems.childCount; i++)
        {
            elems.GetChild(i).GetChild(0).gameObject.SetActive(true);
            elems.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }
}
