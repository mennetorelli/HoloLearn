using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionManager : MonoBehaviour {

    private float lerpPercentage;
    private bool hasCollided;

    private Vector3 finalPosition;
    private Quaternion finalRotation;

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
            //transform.position = Vector3.Lerp(transform.position, finalPosition, lerpPercentage);
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, lerpPercentage);
            Debug.Log(lerpPercentage);
        }
    }

    public void AdjustTransform(Transform target)
    {
        hasCollided = true;

        finalPosition = target.position;
        transform.position = target.position;

        finalRotation = target.rotation;

    }
}
