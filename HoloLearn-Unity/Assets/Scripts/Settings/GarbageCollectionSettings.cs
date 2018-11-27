using HoloToolkit.Unity;
using System;

public class GarbageCollectionSettings : Singleton<GarbageCollectionSettings>
{
    public int numberOfBins;
    public int numberOfWaste;

    //Per la sperimentazione HQ vs LQ
    public int lowWaste;
}
