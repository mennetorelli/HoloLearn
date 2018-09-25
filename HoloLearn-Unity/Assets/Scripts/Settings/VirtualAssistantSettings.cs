using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantSettings : Singleton<VirtualAssistantSettings>
{
    public bool assistantPresence;
    public int selectedAssistant;
    public int assistantBehaviour;
    public int assistantPatience;
}
