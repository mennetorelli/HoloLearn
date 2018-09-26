﻿using HoloToolkit.Examples.InteractiveElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
        XElement root = SettingsFileManager.Instance.CreateNewXML();

        IEnumerable<XElement> oldSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
            select item.Element("VirtualAssistantSettings");

        XElement newSettings =
            new XElement("VirtualAssistantSettings",
                new XAttribute("AssistantBehaviour", VirtualAssistantSettings.Instance.assistantBehaviour),
                new XAttribute("AssistantPatience", VirtualAssistantSettings.Instance.assistantPatience));

        oldSettings.ElementAt(0).ReplaceWith(newSettings);
        SettingsFileManager.Instance.UpdateFile(root);
    }

}
