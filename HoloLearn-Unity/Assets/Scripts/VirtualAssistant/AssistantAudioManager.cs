using HoloToolkit.Unity;
using UnityEngine;

public class AssistantAudioManager : AssistantAudioManagerInterface
{
    [AudioEvent]
    public string ShakingHeadNo;
    [AudioEvent]
    public string Jump;


    public override void PlayShakingHeadNo()
    {
        UAudioManager.Instance.PlayEvent(ShakingHeadNo);
    }

    public override void PlayJump()
    {
        UAudioManager.Instance.PlayEvent(Jump);
    }

    
    public override void PlayWalking()
    {

    }

    public override void PlayIntro()
    {

    }

    public override void PlayPointing()
    {

    }

}