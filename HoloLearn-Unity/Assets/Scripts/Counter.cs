using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : Singleton<Counter> {

    private int count;



	// Use this for initialization
	void Start ()
    {
        
	}

    public void Decrement()
    {
        count--;
        Debug.Log(count);
    }

    public void InitializeCounter(Transform remainingObjects)
    {
        count = remainingObjects.GetComponentsInChildren<Rigidbody>().Length;
        Debug.Log(count);
    }

    

}
