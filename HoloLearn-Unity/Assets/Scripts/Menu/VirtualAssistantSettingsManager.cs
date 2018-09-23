using HoloToolkit.Examples.InteractiveElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantSettingsManager : MonoBehaviour {

    private int selectedAssistant;
    private int assistantBehaviour;
    private int assistantPatience;

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void SetSelectedAssistant(int selectedAssistant)
    {
        this.selectedAssistant = selectedAssistant;
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
            gameObject.transform.Find("RestDisappear").transform.GetChild(1).gameObject.SetActive(true);
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


    public void LeftArrowClicked()
    {
        if (selectedAssistant != 0)
        {
            GameObject.Find("VirtualAssistants").transform.GetChild(selectedAssistant).gameObject.SetActive(false);
            selectedAssistant--;
            GameObject.Find("VirtualAssistants").transform.GetChild(selectedAssistant).gameObject.SetActive(true);
        }
        RefreshAssistantChoiceMenu();
    }

    public void RightArrowClicked()
    {
        if (selectedAssistant != GameObject.Find("VirtualAssistants").transform.childCount)
        {
            GameObject.Find("VirtualAssistants").transform.GetChild(selectedAssistant).gameObject.SetActive(false);
            selectedAssistant++;
            GameObject.Find("VirtualAssistants").transform.GetChild(selectedAssistant).gameObject.SetActive(true);
        }
        RefreshAssistantChoiceMenu();
    }

    public void RefreshAssistantChoiceMenu()
    {
        GameObject.Find("VirtualAssistants").transform.GetChild(selectedAssistant).gameObject.SetActive(true);

        GameObject.Find("LeftArrow").SetActive(true);
        GameObject.Find("RightArrow").SetActive(true);
        if (selectedAssistant == 0)
        {
            GameObject.Find("LeftArrow").SetActive(false);
        }
        if (selectedAssistant == GameObject.Find("VirtualAssistants").transform.childCount-1)
        {
            GameObject.Find("RightArrow").SetActive(false);
        }
    }


    public void SaveSettings()
    {
        VirtualAssistantSettings.Instance.selectedAssistant = selectedAssistant;
        VirtualAssistantSettings.Instance.assistantBehaviour = assistantBehaviour;
        VirtualAssistantSettings.Instance.assistantPatience = assistantPatience;
    }

    private void LoadSettings()
    {
        selectedAssistant = VirtualAssistantSettings.Instance.selectedAssistant;
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }
}
