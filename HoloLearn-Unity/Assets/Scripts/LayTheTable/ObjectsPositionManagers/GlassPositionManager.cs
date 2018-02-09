using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPositionManager : ObjectPositionManager
{
    
    public override void AdjustTransform()
    {
        transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
    }
}
