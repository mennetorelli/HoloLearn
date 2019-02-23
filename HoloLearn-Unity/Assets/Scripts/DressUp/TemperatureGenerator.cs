using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGenerator : MonoBehaviour {

    public int MinRange;
    public int MaxRange;

	// Use this for initialization
	void Start ()
    {
        Transform weather = GameObject.Find("Weather").transform;
        DressUpManager manager = (DressUpManager)TaskManager.Instance;

        Vector3 temperaturePostion = weather.position;
        Vector3 relativePos = Camera.main.transform.position - temperaturePostion;
        Quaternion temperatureRotation = Quaternion.LookRotation(relativePos);

        int temperature = new System.Random().Next(MinRange, MaxRange);
        int second = temperature % 10;
        int first = (temperature - second) / 10;

        if (first != 0)
        {
            Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(first - 1), temperaturePostion, temperatureRotation, weather);
        }
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(first - 1), temperaturePostion, temperatureRotation, weather);
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(second - 1), temperaturePostion + new Vector3(0.1f, 0f, 0f), temperatureRotation, weather);
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(manager.WeatherPrefabs.transform.GetChild(0).childCount - 2), temperaturePostion + new Vector3(0.2f, 0f, 0f), temperatureRotation, weather);
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(manager.WeatherPrefabs.transform.GetChild(0).childCount - 3), temperaturePostion + new Vector3(0.3f, 0f, 0f), temperatureRotation, weather);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
