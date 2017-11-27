using HoloToolkit.Unity.SpatialMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageCollectionManager : ObjectsManager
{

    public GameObject BinsPrefabs;
    public GameObject WastePrefabs;

    private int numberOfBins;
    private int numberOfObjects;

    private List<string> activeBins = new List<string>();

    // Use this for initialization
    public override void Start()
    {
        numberOfBins = 2;
        numberOfObjects = 10;
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il pavimento
        Transform floor = floors.ElementAt(0).transform;
        SurfacePlane plane = floor.GetComponent<SurfacePlane>();

        System.Random rnd = new System.Random();


      
        Vector3 position = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        Debug.Log(position);
        position = AdjustPositionWithSpatialMap(position, plane.SurfaceNormal);
        Debug.Log(position);
        Quaternion rotation = Camera.main.transform.localRotation;
        Debug.Log(rotation);

        rotation = Quaternion.LookRotation(Camera.main.transform.position);
        rotation.x = 0f;
        rotation.z = 0f;
        Debug.Log(rotation);


        Transform bins = new GameObject("Bins").transform;

        for (int i=1; i<=numberOfBins;)
        {
            Transform bin = BinsPrefabs.transform.GetChild(rnd.Next(0, BinsPrefabs.transform.childCount));
            String currentBinTag = bin.gameObject.tag;
            if (!activeBins.Contains(currentBinTag))
            {
                Instantiate(bin, new Vector3((float)Math.Pow(-1, i) * 0.4f * (i / 2), 0f, 0f), bin.rotation, bins);
                activeBins.Add(bin.gameObject.tag);
                i++;
            }
        }

        bins.Translate(position);
        bins.Rotate(rotation.eulerAngles);


        Transform waste = new GameObject("Waste").transform;

        for (int i=0; i<numberOfObjects;)
        {
            Transform currentWaste = WastePrefabs.transform.GetChild(rnd.Next(0, WastePrefabs.transform.childCount));
            String currentWasteTag = currentWaste.gameObject.tag;
            if (activeBins.Contains(currentWasteTag))
            {
                Instantiate(currentWaste.gameObject, new Vector3(0f, 0f, 0f), currentWaste.rotation, waste);
                i++;
            }
        }

        waste.Translate(position);
        waste.Rotate(rotation.eulerAngles);

    }


    /// <summary>
    /// Adjusts the initial position of the object if it is being occluded by the spatial map.
    /// </summary>
    /// <param name="position">Position of object to adjust.</param>
    /// <param name="surfaceNormal">Normal of surface that the object is positioned against.</param>
    /// <returns></returns>
    private Vector3 AdjustPositionWithSpatialMap(Vector3 position, Vector3 surfaceNormal)
    {
        Vector3 newPosition = position;
        RaycastHit hitInfo;
        float distance = 0.5f;

        // Check to see if there is a SpatialMapping mesh occluding the object at its current position.
        if (Physics.Raycast(position, surfaceNormal, out hitInfo, distance, SpatialMappingManager.Instance.LayerMask))
        {
            // If the object is occluded, reset its position.
            newPosition = hitInfo.point;
        }

        return newPosition;
    }
}
