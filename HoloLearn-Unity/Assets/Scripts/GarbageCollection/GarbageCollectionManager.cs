using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GarbageCollectionManager : TaskManager
{

    public GameObject BinsPrefabs;
    public GameObject WastePrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int numberOfBins;
    private int numberOfWaste;
    private int assistantBehaviour;
    private int assistantPatience;

    private Transform virtualAssistant;

    public List<string> activeBins = new List<string>();

    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(assistantBehaviour);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld()
    {
        //Seleziono il pavimento
        Transform floor = FloorProcessing.Instance.floors.ElementAt(0).transform;
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

        Vector3 binsPosition = gazePosition;
        binsPosition.y = floorPosition.y;


        Vector3 relativePos = Camera.main.transform.position - gazePosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;


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

        Vector3 wastePosition = Vector3.Lerp(Camera.main.transform.position, bins.position, 0.5f);
        wastePosition.y = floorPosition.y + 0.1f;

        for (int i=0; i<numberOfWaste;)
        {
            Transform wasteGroup = WastePrefabs.transform.GetChild(rnd.Next(0, WastePrefabs.transform.childCount));
            int groupSize = wasteGroup.GetComponentsInChildren<Rigidbody>().Length;
            Transform currentWaste = wasteGroup.GetChild(rnd.Next(0, groupSize));
            String currentWasteTag = currentWaste.gameObject.tag;
            if (activeBins.Contains(currentWasteTag))
            {
                Instantiate(currentWaste.gameObject, currentWaste.position, currentWaste.rotation, waste);
                i++;
            }
        }

        waste.Translate(wastePosition);
        waste.Rotate(rotation.eulerAngles);


        Counter.Instance.InitializeCounter(waste);


        Vector3 assistantPosition = bins.TransformPoint(-0.3f, 0f, 0.3f);
        assistantPosition.y = floor.position.y;

        if (assistantBehaviour != 0)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation);
            VirtualAssistantManager.Instance.patience = assistantPatience;
            VirtualAssistantManager.Instance.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
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
        /*if (File.Exists(Application.persistentDataPath + "/garbageCollectionSettings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/garbageCollectionSettings.dat", FileMode.Open);

            GarbageCollectionSettings settings = (GarbageCollectionSettings)bf.Deserialize(file);
            file.Close();

            numberOfBins = settings.numberOfBins;
            numberOfWaste = settings.numberOfWaste;
            assistantBehaviour = settings.asistantBehaviour;
            assistantPatience = settings.assistantPatience;
        }*/

        numberOfBins = GarbageCollectionSettings.Instance.numberOfBins;
        numberOfWaste = GarbageCollectionSettings.Instance.numberOfWaste;
        assistantBehaviour = GarbageCollectionSettings.Instance.asistantBehaviour;
        assistantPatience = GarbageCollectionSettings.Instance.assistantPatience;

        /*List<int> settings = SaveLoad.Instance.ReadSettings();
        numberOfBins = settings.ElementAt(4);
        numberOfWaste = settings.ElementAt(5);
        assistantBehaviour = settings.ElementAt(6);
        assistantPatience = settings.ElementAt(7);*/
    }
}
