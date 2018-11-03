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
        throw new NotImplementedException();
    }
}
