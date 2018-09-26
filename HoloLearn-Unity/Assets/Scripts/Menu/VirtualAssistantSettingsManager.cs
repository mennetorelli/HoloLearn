using HoloToolkit.Examples.InteractiveElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantSettingsManager : MonoBehaviour
{

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        VirtualAssistantSettings.Instance.assistantBehaviour = assistantBehaviour;
        RefreshMenu();
    }

    public void SetAssistantPatience()
    {
        SliderGestureControl slider = GameObject.Find("PatientTime").GetComponentInChildren<SliderGestureControl>();
        VirtualAssistantSettings.Instance.assistantPatience = Convert.ToInt32(slider.SliderValue) + 2;
    }


    public void RefreshMenu()
    {
        InteractiveToggle[] assistantBehaviourButtons = GameObject.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in assistantBehaviourButtons)
        {
            button.SetSelection(false);
        }
        assistantBehaviourButtons[VirtualAssistantSettings.Instance.assistantBehaviour - 1].SetSelection(true);

        if (VirtualAssistantSettings.Instance.assistantBehaviour == 2)
        {
            GameObject.Find("VirtualAssistantMenu").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("VirtualAssistantMenu").transform.GetChild(4).GetComponentInChildren<SliderGestureControl>().SetSliderValue(VirtualAssistantSettings.Instance.assistantPatience);
        }
        else
        {
            GameObject.Find("VirtualAssistantMenu").transform.GetChild(4).gameObject.SetActive(false);
        }
    }


    public void SaveSettings()
    {
        
    }

}
