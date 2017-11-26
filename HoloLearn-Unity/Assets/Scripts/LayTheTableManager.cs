using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LayTheTableManager : Singleton<LayTheTableManager>
{

    public int layTheTableLevel;
    public int numberOfPeople;
    public GameObject layTheTableObjects;

	// Use this for initialization
	void Start () {
        layTheTableLevel = 1;
        numberOfPeople = 2;
    }

    internal void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il primo tavolo per ora, in futuro si potrebbe selezionare il più vicino
        Transform table = tables.ElementAt(0).transform;

        Bounds tableColliderBounds = table.GetColliderBounds();
        Debug.Log(tableColliderBounds);

        Vector3 tableCenter = tableColliderBounds.center;
        Vector3 tableExtents = tableColliderBounds.extents;

       
        Vector3 tableEdge1 = table.TransformPoint(tableColliderBounds.extents.x / 2 , 0f, 0f);
        Debug.Log(tableEdge1);
 
        Vector3 tableEdge2 = table.TransformPoint(-tableColliderBounds.extents.x / 2 , 0f, 0f);
        Debug.Log(tableEdge2);
       
        Vector3 tableEdge3 = table.TransformPoint(0f, 0f, tableColliderBounds.extents.z / 2);
        Debug.Log(tableEdge3);

        Vector3 tableEdge4 = table.TransformPoint(0f, 0f, -tableColliderBounds.extents.z / 2);
        Debug.Log(tableEdge4);

        List<Vector3> tableEdges = new List<Vector3>() { tableEdge1, tableEdge2, tableEdge3, tableEdge4 };



        List<Quaternion> rotations = new List<Quaternion>();

        for (int i=0; i<tableEdges.Count; i++)
        {
            Vector3 relativeDirection = tableEdges.ElementAt(i) - tableCenter;
            Quaternion rotation = Quaternion.LookRotation(relativeDirection);
            rotations.Add(rotation);
        }
        
        Debug.Log(rotations.ElementAt(0));
        Debug.Log(rotations.ElementAt(1));
        Debug.Log(rotations.ElementAt(2));
        Debug.Log(rotations.ElementAt(3));



        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = layTheTableObjects.transform.GetChild(0);
        
        System.Random rnd = new System.Random();

        Transform plates = objectsToBePlaced.Find("Plates");
        Transform plate = plates.GetChild(0);
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(plate.gameObject, tableEdge1 + new Vector3(0.0f, 0.1f, 0.0f), rotations.ElementAt(0));
        }
        
        Transform glasses = objectsToBePlaced.Find("Glasses");
        Transform glassType = glasses.GetChild(rnd.Next(0, glasses.childCount - 1));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(glassType.gameObject, tableEdge1 + new Vector3(0.1f, 0.1f, 0.0f), rotations.ElementAt(0));
        }
        
        Transform cutlery = objectsToBePlaced.Find("Cutlery");
        Transform cutleryType1 = cutlery.Find("Fork");
        Transform cutleryType2 = cutlery.GetChild(rnd.Next(1, 3));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(cutleryType1.gameObject, tableEdge1 + new Vector3(-0.3f, 0.01f, 0.0f), rotations.ElementAt(0));
            Instantiate(cutleryType2.gameObject, tableEdge1 + new Vector3(-0.35f, 0.2f, 0.0f), rotations.ElementAt(0));
        }
        
        Transform beverages = objectsToBePlaced.Find("Beverages");
        //Transform bottle = beverages.Find("WaterBottle");
        //Instantiate(bottle.gameObject, tableCorner + new Vector3(-0.1f, 0.1f, 0.2f), bottle.transform.rotation);
        Transform can = beverages.GetChild(rnd.Next(1, 3));
        Instantiate(can.gameObject, tableEdge1 + new Vector3(-0.1f, 0.1f, 0.2f), rotations.ElementAt(0));




        Transform counter = layTheTableObjects.transform.GetChild(1).Find("Counter");

        //Da creare un metodo per posizionare gli oggetti
        Transform placements = layTheTableObjects.transform.GetChild(1).GetChild(layTheTableLevel-1);

        Transform tableMatesPlacements = placements.Find("TableMatesPlacements");
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(tableMatesPlacements.gameObject, tableEdges.ElementAt(i+1), rotations.ElementAt(i+1), counter);
        }

        Transform beveragesPlacements = placements.Find("BeveragesPlacements");
        Instantiate(beveragesPlacements.gameObject, tableCenter, beveragesPlacements.transform.rotation, counter);

        Instantiate(counter.gameObject);
    }
}
