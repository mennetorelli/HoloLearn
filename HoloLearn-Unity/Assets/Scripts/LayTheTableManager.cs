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

    internal void GenerateObjectsInWorls(List<GameObject> floors)
    {
        
        floors = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Floor);
        Debug.Log("piani trovati");
        GameObject table = floors.ElementAt(0);

        //Per scegliere a seconda del livello
        Transform selectedLevel = layTheTableObjects.transform.GetChild(LAY_THE_TABLE_LEVEL - 1);

        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = selectedLevel.transform.GetChild(0);
        Instantiate(objectsToBePlaced.gameObject, objectsToBePlaced.position, objectsToBePlaced.rotation);

        //Da creare un metodo per posizionare gli oggetti
        Transform placements = selectedLevel.transform.GetChild(1);
        Instantiate(placements.gameObject, placements.position, placements.rotation);
    }
}
