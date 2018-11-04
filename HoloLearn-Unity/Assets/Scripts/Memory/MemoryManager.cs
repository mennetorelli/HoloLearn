using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryManager : TaskManager
{
    public GameObject BoxPrefab;
    public GameObject ObjectsPrefabs;
    public GameObject PlayModesPrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int playMode;
    private int numberOfBoxes;
    private int waitingTime;
    private int assistantPresence;
    private int selectedAssistant;

    private Transform virtualAssistant;

    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        Instantiate(PlayModesPrefabs.transform.GetChild(playMode), GameObject.Find("MemoryManager").transform);

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant + 1).GetChild(0);
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


        List<Transform> objs = transform.GetComponentInChildren<PlayModeManager>().GenerateObjects(ObjectsPrefabs, numberOfBoxes);

        System.Random rnd = new System.Random();
        Transform elems = new GameObject("Elements").transform;
        for (int i = 1; i <= numberOfBoxes / 2; i++)
        {
            Transform elem = new GameObject("Element").transform;
            elem.parent = elems;
            GameObject box = Instantiate(BoxPrefab, new Vector3((float)Math.Pow(-1, i) * 0.25f * (i / 2), 0f, 0f), BoxPrefab.transform.rotation, elem);
            int j = rnd.Next(0, objs.Count);
            Transform obj = Instantiate(objs.ElementAt(j), box.transform.position, box.transform.rotation, elem);
            obj.gameObject.SetActive(false);
            objs.RemoveAt(j);

            Transform elem2 = new GameObject("Element").transform;
            elem2.parent = elems;
            GameObject box2 = Instantiate(BoxPrefab, new Vector3((float)Math.Pow(-1, i) * 0.25f * (i / 2), 0f, 0.25f), BoxPrefab.transform.rotation, elem2);
            int k = rnd.Next(0, objs.Count);
            Transform obj2 = Instantiate(objs.ElementAt(k), box2.transform.position, box2.transform.rotation, elem2);
            obj2.gameObject.SetActive(false);
            objs.RemoveAt(k);
        }

        elems.Translate(boxesPosition);
        elems.Rotate(rotation.eulerAngles);
        

        Vector3 assistantPosition = elems.TransformPoint(-0.3f, 0f, 0.3f);
        assistantPosition.y = floor.position.y;

        if (assistantPresence != 0)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation);
            VirtualAssistantManager.Instance.transform.localScale += new Vector3(0.25f * VirtualAssistantManager.Instance.transform.localScale.x, 0.25f * VirtualAssistantManager.Instance.transform.localScale.y, 0.25f * VirtualAssistantManager.Instance.transform.localScale.z);
        }

        transform.GetComponentInChildren<PlayModeManager>().StartGame(waitingTime);
        
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
        waitingTime = MemorySettings.Instance.waitingTime;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
    }

}
