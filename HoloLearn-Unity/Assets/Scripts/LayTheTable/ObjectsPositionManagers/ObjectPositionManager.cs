using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPositionManager : MonoBehaviour {

    private float lerpPercentage;
    private bool hasCollided;

    protected Vector3 finalPosition;
    protected Quaternion finalRotation;

    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void HasCollided(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        AdjustTransform();
    }

    public virtual void AdjustTransform()
    {
        return;
    }
}
