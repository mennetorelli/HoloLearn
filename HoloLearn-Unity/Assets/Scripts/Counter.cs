using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    private int objectsToPositionate;
    public Text countText;



	// Use this for initialization
	void Start () {
        objectsToPositionate = GetComponentsInChildren<Rigidbody>().Length;
        Debug.Log(objectsToPositionate);
	}

    public void Decrement()
    {
        objectsToPositionate--;
        Debug.Log(objectsToPositionate);
        countText.text = objectsToPositionate.ToString();
    }

    

}
