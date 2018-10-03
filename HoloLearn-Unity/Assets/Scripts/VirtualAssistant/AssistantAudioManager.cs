using HoloToolkit.Unity;
using System.Collections;
using UnityEngine;

public class AssistantAudioManager : MonoBehaviour
{
    [AudioEvent]
    public string ShakingHeadNo;
    [AudioEvent]
    public string Jump;

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

    public void PlayTextToSpeech()
    {
        GetComponent<TextToSpeech>().StartSpeaking("Hi, I'm Ty, your virtual assistant");
    }

}