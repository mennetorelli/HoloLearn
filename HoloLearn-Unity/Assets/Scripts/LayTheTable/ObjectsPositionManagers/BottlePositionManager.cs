using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePositionManager : ObjectPositionManager
{
    
    public override void AdjustTransform()
    {
        transform.Rotate(new Vector3(180f, 0f, 0f), Space.Self);
    }
}
