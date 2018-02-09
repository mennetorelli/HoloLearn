using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutleryPositionManager : ObjectPositionManager
{
    
    public override void AdjustTransform()
    {
        transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
    }
}
