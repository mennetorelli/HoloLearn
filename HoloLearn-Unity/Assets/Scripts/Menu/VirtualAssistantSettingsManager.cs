using HoloToolkit.Examples.InteractiveElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantSettingsManager : MonoBehaviour {

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void SetSelectedAssistant(int selectedAssistant)
    {
        VirtualAssistantSettings.Instance.selectedAssistant = selectedAssistant;
    }

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        VirtualAssistantSettings.Instance.assistantBehaviour = assistantBehaviour;
    }

    public void SetAssistantPatience()
    {
        SliderGestureControl slider = gameObject.transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<SliderGestureControl>();
        VirtualAssistantSettings.Instance.assistantPatience = Convert.ToInt32(slider.SliderValue) + 2;
    }


    public void RefreshMenu()
    {
        InteractiveToggle assistantCheckBox = gameObject.transform.Find("AssistantCheckBox").GetComponent<InteractiveToggle>();
        if (VirtualAssistantSettings.Instance.assistantBehaviour != 0)
        {
            assistantCheckBox.SetSelection(true);
            gameObject.transform.Find("RestDisappear").transform.GetChild(1).gameObject.SetActive(true);
            InteractiveToggle[] assistantBehaviourButtons = gameObject.transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
            assistantBehaviourButtons[VirtualAssistantSettings.Instance.assistantBehaviour - 1].SetSelection(true);
            if (VirtualAssistantSettings.Instance.assistantBehaviour == 2)
            {
                transform.Find("RestDisappear").transform.GetChild(2).gameObject.SetActive(true);
                transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<SliderGestureControl>().SetSliderValue(VirtualAssistantSettings.Instance.assistantPatience);
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


    public void LeftArrowClicked()
    {
        if (VirtualAssistantSettings.Instance.selectedAssistant != 0)
        {
            GameObject.Find("VirtualAssistants").transform.GetChild(VirtualAssistantSettings.Instance.selectedAssistant).gameObject.SetActive(false);
            VirtualAssistantSettings.Instance.selectedAssistant--;
            GameObject.Find("VirtualAssistants").transform.GetChild(VirtualAssistantSettings.Instance.selectedAssistant).gameObject.SetActive(true);
        }
        RefreshAssistantChoiceMenu();
    }

    public void RightArrowClicked()
    {
        if (VirtualAssistantSettings.Instance.selectedAssistant != GameObject.Find("VirtualAssistants").transform.childCount)
        {
            GameObject.Find("VirtualAssistants").transform.GetChild(VirtualAssistantSettings.Instance.selectedAssistant).gameObject.SetActive(false);
            VirtualAssistantSettings.Instance.selectedAssistant++;
            GameObject.Find("VirtualAssistants").transform.GetChild(VirtualAssistantSettings.Instance.selectedAssistant).gameObject.SetActive(true);
        }
        RefreshAssistantChoiceMenu();
    }

    public void RefreshAssistantChoiceMenu()
    {
        GameObject.Find("VirtualAssistants").transform.GetChild(VirtualAssistantSettings.Instance.selectedAssistant).gameObject.SetActive(true);

        GameObject.Find("LeftArrow").SetActive(true);
        GameObject.Find("RightArrow").SetActive(true);
        if (VirtualAssistantSettings.Instance.selectedAssistant == 0)
        {
            GameObject.Find("LeftArrow").SetActive(false);
        }
        if (VirtualAssistantSettings.Instance.selectedAssistant == GameObject.Find("VirtualAssistants").transform.childCount-1)
        {
            GameObject.Find("RightArrow").SetActive(false);
        }
    }


    public void SaveSettings()
    {
        
    }

    private void LoadSettings()
    {
        
    }
}
