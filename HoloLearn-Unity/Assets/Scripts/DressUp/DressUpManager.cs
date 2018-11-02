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

    private int playerGender;
    private int numberOfLevel;
    private int numberOfClothes;
    private int assistantPresence;
    private int selectedAssistant;
    private int assistantBehaviour;
    private int assistantPatience;

    private Transform virtualAssistant;

    public List<string> activeWeatherTags = new List<string>();


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

        Transform weatherGroup = WeatherPrefabs.transform.GetChild(0);
        Transform selectedWeather = weatherGroup.GetChild(rnd.Next(0, weatherGroup.childCount));
        Instantiate(selectedWeather, weatherPosition, rotation, weather);

        Transform temperatureGroup = WeatherPrefabs.transform.GetChild(1);
        Transform selectedTemperature = temperatureGroup.GetChild(rnd.Next(0, temperatureGroup.childCount));
        Instantiate(selectedTemperature, weatherPosition, rotation, weather);

        GameObject activeWeatherElements = GameObject.Find("Weather");
        for (int i = 0; i < activeWeatherElements.transform.childCount; i++)
        {
            activeWeatherTags.Add(activeWeatherElements.transform.GetChild(i).tag);
        } 

        Transform clothes = new GameObject("Clothes").transform;

        Vector3 clothesPosition = weatherPosition;
        clothesPosition.y = floorPosition.y + 0.1f;
        Debug.DrawLine(weatherPosition, clothesPosition, Color.red, 30f);

        for (int i = 0; i < numberOfClothes; i++)
        {
            Transform currentGroup = ClothesPrefabs.transform.GetChild(rnd.Next(0, ClothesPrefabs.transform.childCount));
            Transform currentClothe = currentGroup.GetChild(rnd.Next(0, currentGroup.childCount));
            Instantiate(currentClothe.gameObject, currentClothe.position, currentClothe.rotation, clothes);
        }

        clothes.Translate(clothesPosition);
        clothes.Rotate(rotation.eulerAngles);


        Vector3 bagPosition = Vector3.Lerp(Camera.main.transform.position, clothes.position, 0.3f);
        bagPosition.y = floorPosition.y + 0.1f;
        Instantiate(BagsPrefabs.transform.GetChild(rnd.Next(0, BagsPrefabs.transform.childCount)).gameObject, bagPosition, rotation);
        Debug.DrawLine(clothesPosition, bagPosition, Color.blue, 30f);


        Counter.Instance.InitializeCounter(clothes.GetComponentsInChildren<Rigidbody>().Length);


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
        playerGender = DressUpSettings.Instance.playerGender;
        numberOfLevel = DressUpSettings.Instance.numberOfLevel;
        numberOfClothes = DressUpSettings.Instance.numberOfClothes;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }
}
