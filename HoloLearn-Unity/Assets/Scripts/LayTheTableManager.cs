using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LayTheTableManager : Singleton<LayTheTableManager>
{

    public static int LAY_THE_TABLE_LEVEL;
    public GameObject layTheTableObjects;

	// Use this for initialization
	void Start () {
        LAY_THE_TABLE_LEVEL = 2;
    }

    internal void GenerateObjectsInWorls(List<GameObject> floors, List<GameObject> tables)
    {
        //Per scegliere a seconda del livello
        Transform selectedLevel = layTheTableObjects.transform.GetChild(LAY_THE_TABLE_LEVEL - 1);

        //Seleziono il primo tavolo per ora, in futuro si potrebbe selezionare il più vicino
        GameObject table = tables.ElementAt(0);

        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = selectedLevel.transform.GetChild(0);
        Instantiate(objectsToBePlaced.gameObject, objectsToBePlaced.transform.position + new Vector3(0.0f, 0.1f, 0.0f), objectsToBePlaced.transform.rotation);

        //Da creare un metodo per posizionare gli oggetti
        Transform placements = selectedLevel.transform.GetChild(1);
        Instantiate(placements.gameObject, table.transform.position, placements.transform.rotation);
    }
}
