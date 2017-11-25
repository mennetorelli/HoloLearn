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
        objectsToBePlaced.gameObject.SetActive(true);
        System.Random rnd = new System.Random();
        objectsToBePlaced.transform.GetChild(rnd.Next(3, 5)).gameObject.SetActive(false);
        objectsToBePlaced.transform.GetChild(rnd.Next(5, 7)).gameObject.SetActive(false);
        Instantiate(objectsToBePlaced.gameObject, objectsToBePlaced.transform.position + new Vector3(0.0f, 0.1f, 0.0f), objectsToBePlaced.transform.rotation);
        


        //Da creare un metodo per posizionare gli oggetti
        Transform placements = layTheTableObjects.transform.GetChild(1);
        Instantiate(placements.gameObject, placements.transform.position, placements.transform.rotation);
    }
}
