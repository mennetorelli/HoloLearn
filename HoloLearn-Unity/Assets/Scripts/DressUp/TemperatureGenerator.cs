﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGenerator : MonoBehaviour {

    public int MinRange;
    public int MaxRange;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateTemperature()
    {
        Transform weather = GameObject.Find("Weather").transform;
        DressUpManager manager = (DressUpManager)TaskManager.Instance;

        Vector3 temperaturePostion = weather.position;
        Vector3 relativePos = Camera.main.transform.position - temperaturePostion;
        Quaternion temperatureRotation = Quaternion.LookRotation(relativePos);

        int temperature = new System.Random().Next(MinRange, MaxRange);
        int unit = temperature % 10;
        int dec = (temperature - unit) / 10;

        if (dec != 0)
        {
            Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(dec), temperaturePostion, temperatureRotation, weather.GetChild(0).GetChild(1));
        }
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(unit), temperaturePostion + new Vector3(0.1f, 0f, 0f), temperatureRotation, weather.GetChild(0).GetChild(1));
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(manager.WeatherPrefabs.transform.GetChild(0).childCount - 2), temperaturePostion + new Vector3(0.2f, 0f, 0f), temperatureRotation, weather.GetChild(0).GetChild(1));
        Instantiate(manager.WeatherPrefabs.transform.GetChild(0).GetChild(manager.WeatherPrefabs.transform.GetChild(0).childCount - 1), temperaturePostion + new Vector3(0.3f, 0f, 0f), temperatureRotation, weather.GetChild(0).GetChild(1));

    }
}
