﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour {
    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement);
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Cylinder"))
       
        {
            gameObject.SetActive(false);
        }        
    } 
}
