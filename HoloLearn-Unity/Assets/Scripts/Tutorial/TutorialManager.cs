using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TutorialManager : TaskManager
{

    public GameObject ObjectsPrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int assistantPresence;
    private int selectedAssistant;
    private int assistantBehaviour;
    private int assistantPatience;

    private Transform virtualAssistant;


    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant + 1).GetChild(assistantBehaviour - 1);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld()
    {
        Transform floor = SpatialProcessing.Instance.floors.ElementAt(0).transform;
        SurfacePlane plane = floor.GetComponent<SurfacePlane>();

        if (SpatialProcessing.Instance.tables.Count != 0)
        {
            Transform table = TableSelect(SpatialProcessing.Instance.tables);
        }

        System.Random rnd = new System.Random();
        

        Vector3 floorPosition = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        floorPosition = AdjustPositionWithSpatialMap(floorPosition, plane.SurfaceNormal);

        Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        {
            gazePosition = hitInfo.point;
        }

        Vector3 objsPosition = gazePosition;
        objsPosition.y = floorPosition.y;


        Vector3 relativePos = Camera.main.transform.position - gazePosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;


        Transform objs = new GameObject("Objects").transform;

        for (int i = 0; i < ObjectsPrefabs.transform.childCount; i++)
        {
            Transform obj = ObjectsPrefabs.transform.GetChild(i);
            Instantiate(obj.gameObject, new Vector3(0f, 0.01f, 0f), obj.rotation, objs);
        }

        objs.Translate(objsPosition);
        objs.Rotate(rotation.eulerAngles);


        Counter.Instance.InitializeCounter(objs.GetComponentsInChildren<Rigidbody>().Length);


        Vector3 assistantPosition = gazePosition;
        assistantPosition.y = floor.position.y;

        if (assistantPresence != 0)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation);
            VirtualAssistantManager.Instance.patience = assistantPatience;
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


    private Transform TableSelect(List<GameObject> tables)
    {
        Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        {
            gazePosition = hitInfo.point;
        }

        float minDistance = 1000f;
        Transform nearestTable = null;
        foreach (GameObject table in tables)
        {
            Vector3 tableCenter = table.transform.GetColliderBounds().center;
            if (Vector3.Distance(tableCenter, gazePosition) <= minDistance)
            {
                minDistance = Vector3.Distance(tableCenter, gazePosition);
                nearestTable = table.transform;
            }
        }

        foreach (GameObject table in tables)
        {
            if (table.GetInstanceID() != nearestTable.gameObject.GetInstanceID())
            {
                Destroy(table.gameObject);
            }
        }

        return nearestTable;
    }


    private void LoadSettings()
    {
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }
}
