using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    private int objectsToPositionate;

	// Use this for initialization
	void Start () {
        objectsToPositionate = GetComponentsInChildren<Rigidbody>().Length;
        Debug.Log(objectsToPositionate);
	}

    public void Decrement()
    {
        objectsToPositionate--;
        Debug.Log(objectsToPositionate);
    }
}
