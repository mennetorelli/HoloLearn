using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPositionManager : MonoBehaviour {

    private float lerpPercentage;
    private bool hasCollided;

    protected Vector3 finalPosition;
    protected Quaternion finalRotation;

    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void HasCollided(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        AdjustTransform();

        gameObject.GetComponent<GravityManager>().IsFixed = true;
    }

    public virtual void AdjustTransform()
    {
        return;
    }
}
