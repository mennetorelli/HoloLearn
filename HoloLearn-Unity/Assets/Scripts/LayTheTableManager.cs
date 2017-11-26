using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LayTheTableManager : Singleton<LayTheTableManager>
{
    public GameObject LevelsPrefab;
    public GameObject ObjectsPrefab;

    private int numberOfPeople;
    private int numberOfLevel;

    private Transform selectedLevel;

    // Use this for initialization
    void Start () {
        numberOfLevel = 1;
        numberOfPeople = 3;

        selectedLevel = LevelsPrefab.transform.GetChild(numberOfLevel-1);
    }

    internal void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il primo tavolo per ora, in futuro si potrebbe selezionare il più vicino
        Transform table = tables.ElementAt(0).transform;


        Bounds tableColliderBounds = table.GetColliderBounds();
        Debug.Log(tableColliderBounds);
        
       
        Vector3 tableEdge1 = table.TransformPoint(tableColliderBounds.extents.x / 2 - 0.2f, 0f, 0f);
        Vector3 tableEdge2 = table.TransformPoint(-tableColliderBounds.extents.x / 2 , 0f, 0f);
        Vector3 tableEdge3 = table.TransformPoint(0f, tableColliderBounds.extents.z / 2, 0f);
        Vector3 tableEdge4 = table.TransformPoint(0f, -tableColliderBounds.extents.z / 2, 0f);

        List<Vector3> tableEdges = new List<Vector3>() { tableEdge1, tableEdge2, tableEdge3, tableEdge4 };

        

        List<Quaternion> rotations = new List<Quaternion>();

        for (int i=0; i<tableEdges.Count; i++)
        {
            Vector3 relativeDirection = tableColliderBounds.center - tableEdges.ElementAt(i);
            Quaternion rotation = Quaternion.LookRotation(relativeDirection);
            rotations.Add(rotation);
        }


        Transform objectsToBePlaced = selectedLevel.gameObject.GetComponent<LayTheTableObjectsGeneratorLvl1>().GenerateObjects(ObjectsPrefab.transform, numberOfPeople);
        objectsToBePlaced.Translate(tableEdge1);
        objectsToBePlaced.Rotate(rotations.ElementAt(0).eulerAngles);


        Transform counter = LevelsPrefab.transform.Find("Counter");

        Transform tableMatesPlacements = selectedLevel.Find("TableMatePlacement");
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(tableMatesPlacements.gameObject, tableEdges.ElementAt(i+1), rotations.ElementAt(i+1), counter);
        }

        Transform beveragesPlacements = selectedLevel.Find("BeveragesPlacement");
        Instantiate(beveragesPlacements.gameObject, tableColliderBounds.center, beveragesPlacements.transform.rotation, counter);

        Instantiate(counter.gameObject);
    }
}
