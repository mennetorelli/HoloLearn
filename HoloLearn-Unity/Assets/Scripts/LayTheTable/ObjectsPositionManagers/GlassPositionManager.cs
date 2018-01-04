using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPositionManager : ObjectPositionManager
{
    
    public override void AdjustTransform()
    {
        finalRotation.x = -90f;
    }
}
