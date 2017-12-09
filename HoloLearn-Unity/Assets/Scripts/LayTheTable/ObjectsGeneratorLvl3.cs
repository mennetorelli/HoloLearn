﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGeneratorLvl3 : ObjectsGenerator{

	// Use this for initialization
	public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void  Update () {
		
	}

    public override Transform GenerateObjects(Transform objectsPrefab, int numberOfPeople)
    {
        System.Random rnd = new System.Random();

        Transform objectsToBePlaced = new GameObject("ObjectsToBePlaced").transform;

        Transform plates = objectsPrefab.Find("Plates");
        Transform plate = plates.GetChild(0);
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(plate.gameObject, new Vector3(0.0f, 0.1f, 0.0f), plate.transform.rotation, objectsToBePlaced);
        }

        Transform glasses = objectsPrefab.Find("Glasses");
        Transform glassType = glasses.GetChild(rnd.Next(0, glasses.childCount - 1));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(glassType.gameObject, new Vector3(0.1f, 0.1f, 0.0f), glassType.transform.rotation, objectsToBePlaced);
        }

        Transform cutlery = objectsPrefab.Find("Cutlery");
        Transform cutleryType1 = cutlery.Find("Fork");
        Transform cutleryType2 = cutlery.Find("Knife"); 
        Transform cutleryType3 = cutlery.Find("Tablespoon"); 
            Instantiate(cutleryType1.gameObject, new Vector3(-0.3f, 0.01f, 0.0f), cutleryType1.transform.rotation, objectsToBePlaced);
            Instantiate(cutleryType2.gameObject, new Vector3(-0.35f, 0.2f, 0.0f), cutleryType2.transform.rotation, objectsToBePlaced);
            Instantiate(cutleryType3.gameObject, new Vector3(-0.4f, 0.3f, 0.0f), cutleryType3.transform.rotation, objectsToBePlaced);

        

        Transform beverages = objectsPrefab.Find("Beverages");
        //Transform bottle = beverages.Find("WaterBottle");
        //Instantiate(bottle.gameObject, tableCorner + new Vector3(-0.1f, 0.1f, 0.2f), bottle.transform.rotation);
        Transform can1 = beverages.GetChild(rnd.Next(1, 3));
        Transform can2 = beverages.GetChild(rnd.Next(1, 3));
        Instantiate(can1.gameObject, new Vector3(-0.1f, 0.1f, 0.2f), can1.transform.rotation, objectsToBePlaced);
        Instantiate(can2.gameObject, new Vector3(-0.15f, 0.15f, 0.2f), can2.transform.rotation, objectsToBePlaced);

        return objectsToBePlaced;
    }



}
