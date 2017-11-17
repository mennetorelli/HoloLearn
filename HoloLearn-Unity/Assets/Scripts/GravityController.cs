using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

    private Rigidbody rb;
    private CustomHandDraggable hd;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        hd = gameObject.GetComponent<CustomHandDraggable>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hd.isDragging)
        {
            rb.useGravity = false;
        }

        if (!hd.isDragging)
        {
            rb.useGravity = true;
        }
    }
}
