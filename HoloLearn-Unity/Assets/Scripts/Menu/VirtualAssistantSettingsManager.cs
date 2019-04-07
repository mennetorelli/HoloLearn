using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class VirtualAssistantSettingsManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetExplainTaskGoal(int explainTaskGoal)
    {
        VirtualAssistantSettings.Instance.explainTaskGoal = explainTaskGoal;
        RefreshMenu();
    }

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        VirtualAssistantSettings.Instance.assistantBehaviour = assistantBehaviour;
        RefreshMenu();
    }

    public void SetAssistantPatience()
    {
        //SliderGestureControl slider = GameObject.Find("PatienceTime").GetComponentInChildren<SliderGestureControl>();
        //VirtualAssistantSettings.Instance.assistantPatience = Convert.ToInt32(slider.SliderValue) + 2;
    }


    public void RefreshMenu()
    {
        Interactable targetCheckBox = gameObject.transform.Find("TaskIntroCheckBox").GetComponent<Interactable>();
        if (VirtualAssistantSettings.Instance.explainTaskGoal == 1)
        {
            targetCheckBox.SetToggled(true);
        }
        else
        {
            targetCheckBox.SetToggled(false);
        }

        Interactable[] assistantBehaviourButtons = GameObject.Find("AssistantBehaviourButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in assistantBehaviourButtons)
        {
            button.SetToggled(false);
        }
        assistantBehaviourButtons[VirtualAssistantSettings.Instance.assistantBehaviour - 1].SetToggled(true);

        if (VirtualAssistantSettings.Instance.assistantBehaviour == 2)
        {
            GameObject.Find("VirtualAssistantSettings").transform.GetChild(5).gameObject.SetActive(true);
            //GameObject.Find("VirtualAssistantSettings").transform.GetChild(5).GetComponentInChildren<SliderGestureControl>().SetSliderValue(VirtualAssistantSettings.Instance.assistantPatience);
        }
        else
        {
            GameObject.Find("VirtualAssistantSettings").transform.GetChild(5).gameObject.SetActive(false);
        }
    }


    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("VirtualAssistantSettings",
                new XAttribute("ExplainTaskGoal", VirtualAssistantSettings.Instance.explainTaskGoal),
                new XAttribute("AssistantBehaviour", VirtualAssistantSettings.Instance.assistantBehaviour),
                new XAttribute("AssistantPatience", VirtualAssistantSettings.Instance.assistantPatience));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }

}
