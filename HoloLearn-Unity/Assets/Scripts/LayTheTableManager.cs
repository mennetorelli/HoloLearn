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
        layTheTableLevel = 2;
        numberOfPeople = 3;

        GenerateObjectsInWorld(null, null);
    }

    internal void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il primo tavolo per ora, in futuro si potrebbe selezionare il più vicino
        //GameObject table = tables.ElementAt(0);

        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = layTheTableObjects.transform.GetChild(0);

        System.Random rnd = new System.Random();

        Transform plates = objectsToBePlaced.Find("Plates");
        for (int i=0; i<numberOfPeople; i++)
        {
            Instantiate(plates.gameObject, plates.transform.position + new Vector3(0.0f, 0.1f, 0.0f), plates.transform.rotation);
        }
        
        Transform glasses = objectsToBePlaced.Find("Glasses");
        Transform glassType = glasses.GetChild(rnd.Next(0, glasses.childCount - 1));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(glassType.gameObject, glassType.transform.position + new Vector3(0.0f, 0.1f, 0.0f), glassType.transform.rotation);
        }
        
        Transform cutlery = objectsToBePlaced.Find("Cutlery");
        Transform cutleryType1 = cutlery.Find("Fork");
        Transform cutleryType2 = cutlery.GetChild(rnd.Next(1, 3));
        for (int i = 0; i < numberOfPeople; i++)
        {
            Instantiate(cutleryType1.gameObject, cutleryType1.transform.position + new Vector3(0.0f, 0.1f, 0.0f), cutleryType1.transform.rotation);
            Instantiate(cutleryType2.gameObject, cutleryType2.transform.position + new Vector3(0.0f, 0.1f, 0.0f), cutleryType2.transform.rotation);
        }
        
        Transform beverages = objectsToBePlaced.Find("Beverages");
        Transform bottle = beverages.Find("WaterBottle");
        Instantiate(bottle.gameObject, bottle.transform.position + new Vector3(0.0f, 0.1f, 0.0f), bottle.transform.rotation);
        Transform can = beverages.GetChild(rnd.Next(1, 3));
        Instantiate(can.gameObject, can.transform.position + new Vector3(0.0f, 0.1f, 0.0f), can.transform.rotation);



        //Da creare un metodo per posizionare gli oggetti
        Transform placements = layTheTableObjects.transform.GetChild(1);
        Instantiate(placements.gameObject, placements.transform.position, placements.transform.rotation);
    }
}
