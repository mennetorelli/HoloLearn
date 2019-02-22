using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionManager : ObjectPositionManager
{
    private bool hasCollided;
    private bool lerpDone;
    private Vector3 targetPosition;
    private Vector3 floorPosition;


    public override void Start()
    {
        hasCollided = false;
        lerpDone = false;
        targetPosition = new Vector3();
        floorPosition = GameObject.Find("SurfacePlane(Clone)").transform.position;
    }

    public override void Update()
    {
        if (transform.position.y < floorPosition.y)
        {
            transform.position = new Vector3(transform.position.x, floorPosition.y + 0.01f, transform.position.z);
        }

        if (hasCollided)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                lerpDone = true;
                hasCollided = false;
                transform.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (lerpDone)
        {
            if (Math.Abs(transform.position.y - floorPosition.y) < 0.2f)
            {
                transform.GetComponentInChildren<MeshCollider>().enabled = true;
                lerpDone = false;
            }
        }
    }


    public override void HasCollided(Transform target)
    {
        transform.GetComponent<CustomHandDraggable>().StopDragging();
        transform.GetComponentInChildren<MeshCollider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;

        targetPosition = target.TransformPoint(target.GetComponentInChildren<BoxCollider>().center);

        hasCollided = true;
    }

}
