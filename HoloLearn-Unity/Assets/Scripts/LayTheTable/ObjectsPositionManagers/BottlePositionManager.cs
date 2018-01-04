using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePositionManager : ObjectPositionManager
{
    
    public override void AdjustTransform()
    {
        finalRotation.x = 180f;
    }
}
