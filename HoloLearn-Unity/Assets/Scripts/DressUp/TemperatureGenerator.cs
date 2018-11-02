using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGenerator : MonoBehaviour {

    public int MinRange;
    public int MaxRange;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<TextMesh>().text = new System.Random().Next(MinRange, MaxRange).ToString() + "°C";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
