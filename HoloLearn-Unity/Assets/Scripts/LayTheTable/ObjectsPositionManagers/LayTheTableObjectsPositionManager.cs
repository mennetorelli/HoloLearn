using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LayTheTableObjectsPositionManager : ObjectPositionManager {

    private float lerpPercentage;
    private bool hasCollided;

    protected Vector3 finalPosition;
    protected Quaternion finalRotation;


    public override void HasCollided(Transform target)
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
