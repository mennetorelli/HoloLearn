using HoloToolkit.Unity;
using UnityEngine;

public class MinionAudioManager : AssistantAudioManagerInterface
{
    [AudioEvent]
    public string ShakingHeadNo;
    [AudioEvent]
    public string Jump;


    public override void PlayShakingHeadNo()
    {
        TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(ShakingHeadNo);
    }

    public override void PlayJump()
    {
        TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Jump);
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