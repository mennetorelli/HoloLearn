using HoloToolkit.Unity.SpatialMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageCollectionManager : TaskManager
{

    public GameObject BinsPrefabs;
    public GameObject WastePrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int numberOfBins;
    private int numberOfObjects;
    private Transform virtualAssistant;

    private List<string> activeBins = new List<string>();

    // Use this for initialization
    public override void Start()
    {
        numberOfBins = 2;
        numberOfObjects = 4;

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(0);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld(List<GameObject> floors)
    {
        //Seleziono il pavimento
        Transform floor = floors.ElementAt(0).transform;
        SurfacePlane plane = floor.GetComponent<SurfacePlane>();

        System.Random rnd = new System.Random();


      
        Vector3 floorPosition = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        Debug.Log(floorPosition);
        floorPosition = AdjustPositionWithSpatialMap(floorPosition, plane.SurfaceNormal);
        Debug.Log(floorPosition);

        Vector3 binsPosition = Camera.main.transform.TransformPoint(new Vector3(0f, 0f, 2f));
        binsPosition.y = floorPosition.y;
        Debug.Log(binsPosition);

        Quaternion rotation = Camera.main.transform.localRotation;
        Debug.Log(rotation);

        rotation = Quaternion.LookRotation(Camera.main.transform.position);
        rotation.x = 0f;
        rotation.z = 0f;
        Debug.Log(rotation);


        Transform bins = new GameObject("Bins").transform;
        bins.tag = "Targets";

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

        bins.Translate(binsPosition);
        bins.Rotate(rotation.eulerAngles);


        Transform waste = new GameObject("Waste").transform;
        waste.tag = "ObjectsToBePlaced";

        for (int i=0; i<numberOfObjects;)
        {
            Transform wasteGroup = WastePrefabs.transform.GetChild(rnd.Next(0, WastePrefabs.transform.childCount));
            int groupSize = wasteGroup.GetComponentsInChildren<Rigidbody>().Length;
            Transform currentWaste = wasteGroup.GetChild(rnd.Next(0, groupSize));
            String currentWasteTag = currentWaste.gameObject.tag;
            if (activeBins.Contains(currentWasteTag))
            {
                Instantiate(currentWaste.gameObject, new Vector3(0f, 0f, 0f), currentWaste.rotation, waste);
                i++;
            }
        }

        Vector3 wastePosition = Camera.main.transform.TransformPoint(new Vector3(0f, 0f, 1f));
        wastePosition.y = floorPosition.y + 0.2f;

        waste.Translate(wastePosition);
        waste.Rotate(rotation.eulerAngles);



        Vector3 assistantPosition = binsPosition + new Vector3(0.3f, 0f, 0f);
        assistantPosition.y = floor.position.y;

        if (virtualAssistant != null)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation);
        }

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
