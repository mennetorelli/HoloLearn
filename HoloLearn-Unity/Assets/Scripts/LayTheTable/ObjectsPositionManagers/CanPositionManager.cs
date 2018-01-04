using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPositionManager : ObjectPositionManager
{
    
    public override void AdjustTransform()
    {
        finalRotation.y = -90f;
    }
}
