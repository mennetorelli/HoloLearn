using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LayTheTableManager : ObjectsManager
{
    public GameObject LevelsPrefabs;
    public GameObject ObjectsPrefabs;

    private int numberOfPeople;
    private int numberOfLevel;

    private Transform selectedLevel;

    // Use this for initialization
    public override void Start() {
        numberOfLevel = 2;
        numberOfPeople = 2;

        selectedLevel = LevelsPrefabs.transform.GetChild(numberOfLevel-1);
    }

    // Update is called once per frame
    public override void Update()
    {
    
    }

    public override void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il primo tavolo per ora, in futuro si potrebbe selezionare il più vicino
        Transform table = tables.ElementAt(0).transform;


        Bounds tableColliderBounds = table.GetColliderBounds();
        Debug.Log(tableColliderBounds);
        
       
        Vector3 tableEdge1 = table.TransformPoint(tableColliderBounds.extents.x / 2 - 0.2f, 0f, 0f);
        Vector3 tableEdge2 = table.TransformPoint(-tableColliderBounds.extents.x / 2 + 0.1f, 0f, 0f);
        Vector3 tableEdge3 = table.TransformPoint(0f, tableColliderBounds.extents.z / 2 - 0.1f, 0f);
        Vector3 tableEdge4 = table.TransformPoint(0f, -tableColliderBounds.extents.z / 2 + 0.1f, 0f);

        List<Vector3> tableEdges = new List<Vector3>() { tableEdge1, tableEdge2, tableEdge3, tableEdge4 };
        

        List<Quaternion> rotations = new List<Quaternion>();

        for (int i=0; i<tableEdges.Count; i++)
        {
            Vector3 relativeDirection = tableColliderBounds.center - tableEdges.ElementAt(i);
            Quaternion rotation = Quaternion.LookRotation(relativeDirection);
            rotations.Add(rotation);
        }



        Transform objectsToBePlaced = selectedLevel.gameObject.GetComponent<ObjectsGenerator>().GenerateObjects(ObjectsPrefabs.transform, numberOfPeople);
        objectsToBePlaced.Translate(tableEdge1);
        objectsToBePlaced.Rotate(rotations.ElementAt(0).eulerAngles);



        Transform tableMatesPlacements = selectedLevel.Find("TableMatePlacement");
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(tableMatesPlacements.gameObject, tableEdges.ElementAt(i+1) + new Vector3(0f, 0.01f, 0f), rotations.ElementAt(i+1));
        }

        Transform beveragesPlacements = selectedLevel.Find("BeveragesPlacement");
        Instantiate(beveragesPlacements.gameObject, tableColliderBounds.center + new Vector3(0f, 0.01f, 0f), beveragesPlacements.transform.rotation);

        FindObjectOfType<Counter>().InitializeCounter(objectsToBePlaced);
    }

    
}
