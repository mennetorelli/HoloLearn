using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBinAudioManager : BinAudioManagerInterface
{
    [AudioEvent]
    public string Plastic;

    public override void PlayBinSound()
    {
        UAudioManager.Instance.PlayEvent(Plastic);
    }

}
