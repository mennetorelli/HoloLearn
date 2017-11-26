﻿using HoloToolkit.Unity;
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


        Vector3 tableCenterInLocalCoordinates = tableCenter.InverseTransformPoint(table.position, table.transform.rotation, new Vector3(1f, 1f, 1f));
        Vector3 tableEdgeInLocalCoordinates = tableCenterInLocalCoordinates + new Vector3(tableColliderBounds.extents.x, 0f, 0f);
        Vector3 tableEdge = tableEdgeInLocalCoordinates.TransformPoint(table.position, table.transform.rotation, new Vector3(1f, 1f, 1f));
        Debug.Log(tableEdge);


        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = layTheTableObjects.transform.GetChild(0);
        
        System.Random rnd = new System.Random();

        Transform plates = objectsToBePlaced.Find("Plates");
        Transform plate = plates.GetChild(0);
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(plate.gameObject, tableEdge + new Vector3(0.0f, 0.1f, 0.0f), plate.transform.rotation);
        }
        
        Transform glasses = objectsToBePlaced.Find("Glasses");
        Transform glassType = glasses.GetChild(rnd.Next(0, glasses.childCount - 1));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(glassType.gameObject, tableEdge + new Vector3(0.1f, 0.1f, 0.0f), glassType.transform.rotation);
        }
        
        Transform cutlery = objectsToBePlaced.Find("Cutlery");
        Transform cutleryType1 = cutlery.Find("Fork");
        Transform cutleryType2 = cutlery.GetChild(rnd.Next(1, 3));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(cutleryType1.gameObject, tableEdge + new Vector3(-0.3f, 0.01f, 0.0f), cutleryType1.transform.rotation);
            Instantiate(cutleryType2.gameObject, tableEdge + new Vector3(-0.35f, 0.2f, 0.0f), cutleryType2.transform.rotation);
        }
        
        Transform beverages = objectsToBePlaced.Find("Beverages");
        //Transform bottle = beverages.Find("WaterBottle");
        //Instantiate(bottle.gameObject, tableCorner + new Vector3(-0.1f, 0.1f, 0.2f), bottle.transform.rotation);
        Transform can = beverages.GetChild(rnd.Next(1, 3));
        Instantiate(can.gameObject, tableEdge + new Vector3(-0.1f, 0.1f, 0.2f), can.transform.rotation);




        Transform counter = layTheTableObjects.transform.GetChild(1).Find("Counter");

        //Da creare un metodo per posizionare gli oggetti
        /*Transform placements = layTheTableObjects.transform.GetChild(1).GetChild(layTheTableLevel-1);

        Transform tableMatesPlacements = placements.Find("TableMatesPlacements");
        Vector3 offset = new Vector3(0f, 0f, -0.2f);
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(tableMatesPlacements.gameObject, tableCenter + offset, tableMatesPlacements.transform.rotation, counter);
            offset = offset + new Vector3(0.5f, 0f, 0f);
        }

        Transform beveragesPlacements = placements.Find("BeveragesPlacements");
        Instantiate(beveragesPlacements.gameObject, tableCenter, beveragesPlacements.transform.rotation, counter);

        Instantiate(counter.gameObject);*/
    }
}
