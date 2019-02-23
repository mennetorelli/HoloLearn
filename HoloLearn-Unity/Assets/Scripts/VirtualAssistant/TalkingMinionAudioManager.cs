﻿using HoloToolkit.Unity;
using System.Collections;
using UnityEngine;

public class TalkingMinionAudioManager : AssistantAudioManagerInterface
{
    private int count;

    [AudioEvent]
    public string Intro;
    [AudioEvent]
    public string Walking_daquestaparte;
    [AudioEvent]
    public string Walking_seguimi;
    [AudioEvent]
    public string Walking_vieniconme;
    [AudioEvent]
    public string Pointing_carta;
    [AudioEvent]
    public string Pointing_plastica;
    [AudioEvent]
    public string Pointing_vetro;
    [AudioEvent]
    public string Pointing_cartabin;
    [AudioEvent]
    public string Pointing_plasticabin;
    [AudioEvent]
    public string Pointing_vetrobin;
    [AudioEvent]
    public string Jumping_benfatto;
    [AudioEvent]
    public string Jumping_eccellente;
    [AudioEvent]
    public string ShakingHeadNo_nonono;
    [AudioEvent]
    public string ShakingHeadNo_riprova;


    public override void PlayShakingHeadNo()
    {

        System.Random rnd = new System.Random();

        switch (rnd.Next(0, 1))
        {
            case 0:
                UAudioManager.Instance.PlayEvent(ShakingHeadNo_nonono);
                break;
            case 1:
                UAudioManager.Instance.PlayEvent(ShakingHeadNo_riprova);
                break;
        }
        count = 0;
    }

    public override void PlayWalking()
    {
        if (count % 5 == 0)
        {
            System.Random rnd = new System.Random();

            switch (rnd.Next(0, 3))
            {
                case 0:
                    UAudioManager.Instance.PlayEvent(Walking_vieniconme);
                    break;
                case 1:
                    UAudioManager.Instance.PlayEvent(Walking_seguimi);
                    break;
                case 2:
                    UAudioManager.Instance.PlayEvent(Walking_daquestaparte);
                    break;
            }
            count++;
        }
    }


    public override void PlayIntro()
    {
        UAudioManager.Instance.PlayEvent(Intro);
        count = 0;
    }

    public override void PlayJump()
    {
        if (count == 0)
        {
            System.Random rnd = new System.Random();

            switch (rnd.Next(0, 1))
            {
                case 0:
                    UAudioManager.Instance.PlayEvent(Jumping_benfatto);
                    break;
                case 1:
                    UAudioManager.Instance.PlayEvent(Jumping_eccellente);
                    break;
            }
            count++;
        }
    }

    public override void PlayPointing()
    {
        GameObject target = VirtualAssistantManager.Instance.targetObject.gameObject;

        if (Vector3.Distance(VirtualAssistantManager.Instance.transform.position, target.transform.position) > 0.05f)
        {
            if (target.name.Contains("Bin"))
            {
                if (target.tag == "Paper")
                    UAudioManager.Instance.PlayEvent(Pointing_cartabin);
                if (target.tag == "Plastic")
                    UAudioManager.Instance.PlayEvent(Pointing_plasticabin);
                if (target.tag == "Glass")
                    UAudioManager.Instance.PlayEvent(Pointing_vetrobin);
            }
            else
            {
                if (target.tag == "Paper")
                    UAudioManager.Instance.PlayEvent(Pointing_carta);
                if (target.tag == "Plastic")
                    UAudioManager.Instance.PlayEvent(Pointing_plastica);
                if (target.tag == "Glass")
                    UAudioManager.Instance.PlayEvent(Pointing_vetro);
            }
        }
        count = 0;
    }

}