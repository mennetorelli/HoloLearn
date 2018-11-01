using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MemoryManager : TaskManager
{

    public GameObject BoxPrefab;
    public GameObject ObjectsPrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int playMode;
    private int numberOfBoxes;
    private int initialWaitingTime;
    private int assistantPresence;
    private int selectedAssistant;

    private Transform virtualAssistant;

    public GameObject selectedElement; 

    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant+1).GetChild(0);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld()
    {
        //Seleziono il pavimento
        Transform floor = SpatialProcessing.Instance.floors.ElementAt(0).transform;
        SurfacePlane plane = floor.GetComponent<SurfacePlane>();

        System.Random rnd = new System.Random();

        
        Vector3 floorPosition = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        floorPosition = AdjustPositionWithSpatialMap(floorPosition, plane.SurfaceNormal);

        Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        {
            gazePosition = hitInfo.point;
        }

        Vector3 boxesPosition = gazePosition;
        boxesPosition.y = floorPosition.y;


        Vector3 relativePos = Camera.main.transform.position - gazePosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;


        Transform elems = new GameObject("Elements").transform;

        List<Transform> objs = new List<Transform>();
        for (int i = 1; i <= numberOfBoxes / 2; i++)
        {
            int j = rnd.Next(0, ObjectsPrefabs.transform.childCount);
            Transform obj = ObjectsPrefabs.transform.GetChild(j);
            objs.Add(obj);
            objs.Add(obj);
        }

        for (int i=1; i<=numberOfBoxes; i++)
        {
            Transform elem = new GameObject("Element").transform;
            elem.parent = elems;
            elem.gameObject.AddComponent<BoxSelectionManager>();

            GameObject box = Instantiate(BoxPrefab, new Vector3((float)Math.Pow(-1, i) * 0.2f * (i / 2), 0f, 0f), BoxPrefab.transform.rotation, elem);

            int j = rnd.Next(0, objs.Count);
            Transform obj = Instantiate(objs.ElementAt(j), box.transform.position, box.transform.rotation, elem);
            obj.gameObject.SetActive(false);
            objs.RemoveAt(j);
        }

        elems.Translate(boxesPosition);
        elems.Rotate(rotation.eulerAngles);


        Counter.Instance.InitializeCounter(elems.childCount);


        Vector3 assistantPosition = elems.TransformPoint(-0.3f, 0f, 0.3f);
        assistantPosition.y = floor.position.y;

        if (assistantPresence != 0)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation);
            VirtualAssistantManager.Instance.transform.localScale += new Vector3(0.25f * VirtualAssistantManager.Instance.transform.localScale.x, 0.25f * VirtualAssistantManager.Instance.transform.localScale.y, 0.25f * VirtualAssistantManager.Instance.transform.localScale.z);
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


    private void LoadSettings()
    {
        playMode = MemorySettings.Instance.playMode;
        numberOfBoxes = MemorySettings.Instance.numberOfBoxes;
        initialWaitingTime = MemorySettings.Instance.initialWaitingTime;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
    }

}
