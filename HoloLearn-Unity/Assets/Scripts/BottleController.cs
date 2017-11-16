using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 newPos;
   
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
        if(other.gameObject.CompareTag("Cylinder"))
        {
            Vector3 newPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            transform.position = newPos;
        }
    }
}
