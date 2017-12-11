using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class VirtualAssistantManager : Singleton<VirtualAssistantManager>
{

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void Idle();

    public abstract void Jump();

    public abstract void ShakeHead();

    public abstract void TargetChanged(GameObject draggedObject);
}
