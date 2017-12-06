using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VirtualAssistantManager : Singleton<VirtualAssistantManager>
{

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void Jump();
}
