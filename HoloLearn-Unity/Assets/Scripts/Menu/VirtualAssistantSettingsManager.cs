using HoloToolkit.Examples.InteractiveElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantSettingsManager : MonoBehaviour {

    public int assistantBehaviour;
    public int assistantPatience;

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        this.assistantBehaviour = assistantBehaviour;
    }

    public void SetAssistantPatience()
    {
        SliderGestureControl slider = gameObject.transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<SliderGestureControl>();
        assistantPatience = Convert.ToInt32(slider.SliderValue) + 2;
    }


    public void RefreshMenu()
    {
        InteractiveToggle assistantCheckBox = gameObject.transform.Find("AssistantCheckBox").GetComponent<InteractiveToggle>();
        if (assistantBehaviour != 0)
        {
            assistantCheckBox.SetSelection(true);
            gameObject.transform.Find("RestDisappear").gameObject.SetActive(true);
            InteractiveToggle[] assistantBehaviourButtons = gameObject.transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
            assistantBehaviourButtons[assistantBehaviour - 1].SetSelection(true);
            if (assistantBehaviour == 2)
            {
                transform.Find("RestDisappear").transform.GetChild(2).gameObject.SetActive(true);
                transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<SliderGestureControl>().SetSliderValue(assistantPatience);
            }
        }
        else
        {
            assistantCheckBox.SetSelection(false);
        }
    }


    public void RefreshAssistantButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();

        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }



    public void SaveSettings()
    {
        VirtualAssistantSettings.Instance.assistantBehaviour = assistantBehaviour;
        VirtualAssistantSettings.Instance.assistantPatience = assistantPatience;
    }

    private void LoadSettings()
    {
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }
}
