using HoloToolkit.Unity;
using System;

//[Serializable]
public class GarbageCollectionSettings : Singleton<GarbageCollectionSettings>
{
    public int numberOfBins;
    public int numberOfWaste;
    public int asistantBehaviour;
    public int assistantPatience;
}
