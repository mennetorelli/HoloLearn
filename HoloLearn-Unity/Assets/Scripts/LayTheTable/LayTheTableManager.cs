using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LayTheTableManager : TaskManager
{
    public GameObject LevelsPrefabs;
    public GameObject ObjectsPrefabs;
    public GameObject VirtualAssistantsPrefabs;
    public GameObject PlacementsManagerPrefabs;

    private int level;
    private int people;
    private int targetsVisibility;
    private int assistant;
    private int patience;

    private Transform virtualAssistant;
    private Transform placementManager;
    private Transform selectedLevel;

    // Use this for initialization
    public override void Start() {
        LoadSettings();

        selectedLevel = LevelsPrefabs.transform.GetChild(level-1);
        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(assistant-1);
        placementManager = PlacementsManagerPrefabs.transform.GetChild(targetsVisibility-1);
        Instantiate(placementManager);
    }

    // Update is called once per frame
    public override void Update()
    {
    
    }



    public override void GenerateObjectsInWorld()
    {
        //Seleziono il tavolo dove guarda l'utente
        Transform table = TableSelect(TableProcessing.Instance.tables);

        Bounds tableColliderBounds = table.GetColliderBounds();
       
        Vector3 tableEdge1 = table.TransformPoint(tableColliderBounds.extents.x / 2 - 0.2f, 0f, 0f);
        Vector3 tableEdge2 = table.TransformPoint(-tableColliderBounds.extents.x / 2 + 0.1f, 0f, 0f);
        Vector3 tableEdge3 = table.TransformPoint(0f, tableColliderBounds.extents.z / 2 - 0.1f, 0f);
        Vector3 tableEdge4 = table.TransformPoint(0f, -tableColliderBounds.extents.z / 2 + 0.1f, 0f);

        List<Vector3> tableEdges = new List<Vector3>() { tableEdge1, tableEdge2, tableEdge3, tableEdge4 };
        Debug.DrawLine(tableEdge1, tableColliderBounds.center, Color.black, 30f);
        Debug.DrawLine(tableEdge2, tableColliderBounds.center, Color.black, 30f);
        Debug.DrawLine(tableEdge3, tableColliderBounds.center, Color.black, 30f);
        Debug.DrawLine(tableEdge4, tableColliderBounds.center, Color.black, 30f);

        List<Quaternion> rotations = new List<Quaternion>();

        for (int i=0; i<tableEdges.Count; i++)
        {
            Vector3 relativeDirection = tableColliderBounds.center - tableEdges.ElementAt(i);
            Quaternion rotation = Quaternion.LookRotation(relativeDirection);
            rotations.Add(rotation);
        }


        Transform objectsToBePlaced = selectedLevel.gameObject.GetComponent<ObjectsGenerator>().GenerateObjects(ObjectsPrefabs.transform, people);
        objectsToBePlaced.Translate(tableEdge1);
        objectsToBePlaced.Rotate(rotations.ElementAt(0).eulerAngles);



        Transform tablePlacements = new GameObject("TableMates").transform;
        tablePlacements.tag = "Targets";

        Transform tableMatesPlacements = selectedLevel.Find("TableMatePlacement");
        for (int i=1; i<=people; i++)
        {
            Instantiate(tableMatesPlacements.gameObject, tableEdges.ElementAt(i) + new Vector3(0f, 0.01f, 0f), rotations.ElementAt(i), tablePlacements);
        }

        Transform beveragesPlacements = selectedLevel.Find("BeveragesPlacement");
        Instantiate(beveragesPlacements.gameObject, tableColliderBounds.center + new Vector3(0f, 0.01f, 0f), beveragesPlacements.transform.rotation, tablePlacements);

        Counter.Instance.InitializeCounter(objectsToBePlaced);



        Vector3 assistantPosition = tableColliderBounds.center + new Vector3(0.3f, 0f, 0f);

        if (virtualAssistant != null)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation);
            VirtualAssistantManager.Instance.patience = patience;
        }
    }

    private Transform TableSelect(List<GameObject> tables)
    {
        Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        {
            gazePosition = hitInfo.point;
            Debug.Log(gazePosition);
        }

        float minDistance = 1000f;
        Transform nearestTable = null;
        foreach (GameObject table in tables)
        {
            Vector3 tableCenter = table.transform.GetColliderBounds().center;
            if (Vector3.Distance(tableCenter, gazePosition) <= minDistance)
            {
                nearestTable = table.transform;
            }
        }
        return nearestTable;
    }
    

    private void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/layTheTableSettings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/layTheTableSettings.dat", FileMode.Open);

            LayTheTableSettings settings = (LayTheTableSettings)bf.Deserialize(file);
            file.Close();

            people = settings.people;
            level = settings.level;
            targetsVisibility = settings.targetsVisibility;
            assistant = settings.assistant;
            patience = settings.patience;
        }
    }
}
