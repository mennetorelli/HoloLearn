using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.SpatialMapping.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DressUpManager : TaskManager
{
    public GameObject WeatherPrefabs;
    public GameObject ClothesPrefabs;
    public GameObject BagsPrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int numberOfLevel;
    private int numberOfClothes;
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

        Vector3 weatherPosition = gazePosition;
        weatherPosition.y = floorPosition.y + 1f;
        Debug.DrawLine(Camera.main.transform.position, weatherPosition, Color.black, 30f);


        Vector3 relativePos = Camera.main.transform.position - gazePosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;

        Transform weather = new GameObject("Weather").transform;
        Transform selectedLevel = WeatherPrefabs.transform.GetChild(numberOfLevel);
        Transform selectedWeather = selectedLevel.GetChild(rnd.Next(0, selectedLevel.childCount));
        Instantiate(selectedWeather, weatherPosition, rotation, weather);

        selectedWeather.GetChild(1).GetComponent<TemperatureGenerator>().GenerateTemperature();


        Transform clothes = new GameObject("Clothes").transform;
        clothes.tag = "ObjectsToBePlaced";

        Vector3 clothesPosition = weatherPosition;
        clothesPosition.y = floorPosition.y + 0.1f;
        Debug.DrawLine(weatherPosition, clothesPosition, Color.red, 30f);

        for (int i = 0; i < numberOfClothes; i++)
        {
            Transform currentClothe = ClothesPrefabs.transform.GetChild(rnd.Next(0, ClothesPrefabs.transform.childCount));
            Instantiate(currentClothe.gameObject, currentClothe.position, currentClothe.rotation, clothes);
        }

        clothes.Translate(clothesPosition);
        clothes.Rotate(rotation.eulerAngles);


        Vector3 bagPosition = Vector3.Lerp(Camera.main.transform.position, clothes.position, 0.3f);
        bagPosition.y = floorPosition.y + 0.1f;
        Instantiate(BagsPrefabs.transform.GetChild(rnd.Next(0, BagsPrefabs.transform.childCount)).gameObject, bagPosition, rotation);
        Debug.DrawLine(clothesPosition, bagPosition, Color.blue, 30f);


        int counter = 0;
        string weathertag = GameObject.Find("Weather").transform.GetChild(0).GetChild(0).tag;
        string temperaturetag = GameObject.Find("Weather").transform.GetChild(0).GetChild(1).tag;
        foreach (Transform obj in clothes)
        {
            List<string> tags = obj.GetComponent<TagsContainer>().tags;
            if (tags.Contains(weathertag) || tags.Contains(temperaturetag))
            {
                counter++;
            }
        }
        Counter.Instance.InitializeCounter(counter);


        Vector3 assistantPosition = clothes.TransformPoint(-0.3f, 0f, 0.3f);
        assistantPosition.y = floor.position.y;
        Debug.DrawLine(bagPosition, assistantPosition, Color.green, 30f);

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


    private void LoadSettings()
    {
        numberOfLevel = DressUpSettings.Instance.numberOfLevel;
        numberOfClothes = DressUpSettings.Instance.numberOfClothes;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }


    public override void DestroyObjects()
    {
        if (VirtualAssistantManager.Instance != null)
        {
            Destroy(VirtualAssistantManager.Instance.gameObject);
        }
        Destroy(GameObject.Find("Weather"));
        Destroy(GameObject.Find("Clothes"));
    }
}
