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

        GameObject table = tables.ElementAt(0);

        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = selectedLevel.transform.GetChild(0);
        Vector3 pos = table.transform.position;
        Debug.Log(pos);
        pos.y= pos.y + 10f;
        Debug.Log(pos);
        Instantiate(objectsToBePlaced.gameObject, pos, table.transform.rotation);

        //Da creare un metodo per posizionare gli oggetti
        Transform placements = selectedLevel.transform.GetChild(1);
        Instantiate(placements.gameObject, table.transform.position, table.transform.rotation);
    }
}
