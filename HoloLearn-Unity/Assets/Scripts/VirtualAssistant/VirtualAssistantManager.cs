using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class VirtualAssistantManager : Singleton<VirtualAssistantManager>
{
    public int patience;
    public Transform targetObject;

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void Jump();

    public abstract void ShakeHead();

    public abstract void ObjectDragged(GameObject draggedObject);

    public abstract void ObjectDropped();

    public abstract void Count();
}
