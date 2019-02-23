using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudioManager : MonoBehaviour
{
    [AudioEvent]
    public string BallBump1;
    [AudioEvent]
    public string BallBump2;
    [AudioEvent]
    public string BallBump3;

    public void PlayBallBump()
    {
        System.Random rnd = new System.Random();
        switch (rnd.Next(0, 2))
        {
            case 0:
                UAudioManager.Instance.PlayEvent(BallBump1);
                break;
            case 1:
                UAudioManager.Instance.PlayEvent(BallBump2);
                break;
            case 2:
                UAudioManager.Instance.PlayEvent(BallBump3);
                break;
        }
    }
}
