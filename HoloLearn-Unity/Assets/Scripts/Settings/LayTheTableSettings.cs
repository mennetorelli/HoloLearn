using HoloToolkit.Unity;
using System;

//[Serializable]
public class LayTheTableSettings : Singleton<LayTheTableSettings>
{
    public int numberOfLevel;
    public int numberOfPeople;
    public int targetsVisibility;
    public int assistantBehaviour;
    public int assistantPatience;
}
