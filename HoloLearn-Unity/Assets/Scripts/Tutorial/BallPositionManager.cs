using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionManager : ObjectPositionManager
{
    private bool hasCollided;
    private Vector3 targetPosition;

    public override void HasCollided(Transform target)
    {
        transform.GetComponentInChildren<MeshCollider>().enabled = false;

        targetPosition = target.GetComponentInChildren<BoxCollider>().center;
        transform.position = targetPosition;

        hasCollided = true;
    }


    public override void Start()
    {
        hasCollided = false;
        targetPosition = new Vector3();
    }

    public override void Update()
    {
        if (hasCollided)
        {
            if (Math.Abs(transform.position.y - targetPosition.y) > 0.2)
            {
                transform.GetComponentInChildren<MeshCollider>().enabled = true;
                transform.GetComponent<CustomHandDraggable>().IsDraggingEnabled = true;
                hasCollided = false;
            }
        }
    }


}
