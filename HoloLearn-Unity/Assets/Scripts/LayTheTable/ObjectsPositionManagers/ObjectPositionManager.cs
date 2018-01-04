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
        hasCollided = false;
        lerpPercentage = 0f;
    }

    // Update is called once per frame
    public void Update()
    {
        if (hasCollided && lerpPercentage < 1)
        {
            lerpPercentage += Time.deltaTime * 2;
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, lerpPercentage);
            Debug.Log(lerpPercentage);
        }
    }

    public void HasCollided(Transform target)
    {
        finalPosition = target.position;
        finalRotation = target.rotation;

        AdjustTransform();

        transform.position = target.position;
    }

    public virtual void AdjustTransform()
    {
        return;
    }
}
