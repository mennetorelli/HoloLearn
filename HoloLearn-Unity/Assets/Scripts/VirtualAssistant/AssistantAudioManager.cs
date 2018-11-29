using HoloToolkit.Unity;
using UnityEngine;

public class AssistantAudioManager : MonoBehaviour
{
    [AudioEvent]
    public string ShakingHeadNo;
    [AudioEvent]
    public string Jump;
    [AudioEvent]
    public string Intro;

    private void Start()
    {

    }

    public void PlayShakingHeadNo()
    {
        UAudioManager.Instance.PlayEvent(ShakingHeadNo);
    }

    public void PlayJump()
    {
        UAudioManager.Instance.PlayEvent(Jump);
    }

    public void PlayIntro()
    {
        UAudioManager.Instance.PlayEvent(Intro);
    }

}