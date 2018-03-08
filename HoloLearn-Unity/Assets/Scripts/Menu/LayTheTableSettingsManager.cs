﻿using HoloLearn;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LayTheTableSettingsManager : MonoBehaviour
{
    private int numberOfLevel;
    private int numberOfPeople;
    private int targetsVisibility;
    private int assistantBehaviour;
    private int assistantPatience;
    

    public void Start()
    {
        LoadSettings();
        RefreshGeneralMenu();
    }

    public void SetNumberOfLevel(int numberOfLevel)
    {
        this.numberOfLevel = numberOfLevel;
    }

    public void SetNumberOfPeople(int numberOfPeople)
    {
        this.numberOfPeople = numberOfPeople;
    }

    public void SetTargetsVisibility(int targetsVisibility)
    {
        this.targetsVisibility = targetsVisibility;
    }

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        this.assistantBehaviour = assistantBehaviour;
    }

    public void SetAssistantPatience()
    {
        SliderGestureControl slider = gameObject.transform.Find("VirtualAssistantLTT").transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<SliderGestureControl>();
        assistantPatience = Convert.ToInt32(slider.SliderValue) + 2;
    }







        public void RefreshLevelsButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("SettingsLTT").transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }

    public void RefreshPeopleButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("SettingsLTT").transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }

    public void RefreshAssistantButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantLTT").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
        
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }


    public void RefreshGeneralMenu()
    {
        InteractiveToggle[] levelButtons = gameObject.transform.Find("SettingsLTT").transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        levelButtons[numberOfLevel - 1].SetSelection(true);

        InteractiveToggle[] peopleButtons = gameObject.transform.Find("SettingsLTT").transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
        peopleButtons[numberOfPeople - 1].SetSelection(true);

        InteractiveToggle targetCheckBox = gameObject.transform.Find("SettingsLTT").transform.Find("TargetCheckBox").GetComponent<InteractiveToggle>();
        if (targetsVisibility==1)
        {
            targetCheckBox.SetSelection(true);
        }
        else
        {
            targetCheckBox.SetSelection(false);
        }

        
    }


    public void RefreshAssistantMenu()
    {
        InteractiveToggle assistantCheckBox = gameObject.transform.Find("VirtualAssistantLTT").transform.Find("AssistantCheckBox").GetComponent<InteractiveToggle>();
        if (assistantBehaviour != 0)
        {
            assistantCheckBox.SetSelection(true);
            gameObject.transform.Find("VirtualAssistantLTT").transform.Find("RestDisappear").gameObject.SetActive(true);
            InteractiveToggle[] assistantBehaviourButtons = gameObject.transform.Find("VirtualAssistantLTT").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
            assistantBehaviourButtons[assistantBehaviour - 1].SetSelection(true);
            if (assistantBehaviour == 2)
            {
                GameObject.Find("VirtualAssistantLTT").transform.Find("RestDisappear").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("VirtualAssistantLTT").transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<SliderGestureControl>().SetSliderValue(assistantPatience);
            }
        }
        else
        {
            assistantCheckBox.SetSelection(false);
        }
    }


    public void SaveSettings()
    {

        /*BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/layTheTableSettings.dat");

        LayTheTableSettings settings = new LayTheTableSettings();
        settings.numberOfLevel = numberOfLevel;
        settings.numberOfPeople = numberOfPeople;
        settings.targetsVisibility = targetsVisibility;
        settings.assistantBehaviour = assistantBehaviour;
        settings.assistantPatience = assistantPatience;

        bf.Serialize(file, settings);
        file.Close();*/

        
        LayTheTableSettings.Instance.numberOfLevel = numberOfLevel;
        LayTheTableSettings.Instance.numberOfPeople = numberOfPeople;
        LayTheTableSettings.Instance.targetsVisibility = targetsVisibility;
        LayTheTableSettings.Instance.assistantBehaviour = assistantBehaviour;
        LayTheTableSettings.Instance.assistantPatience = assistantPatience;
    }

    private void LoadSettings()
    {
        /*if (File.Exists(Application.persistentDataPath + "/layTheTableSettings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/layTheTableSettings.dat", FileMode.Open);

            LayTheTableSettings settings = (LayTheTableSettings)bf.Deserialize(file);
            file.Close();

            numberOfPeople = settings.numberOfPeople;
            numberOfLevel = settings.numberOfLevel;
            targetsVisibility = settings.targetsVisibility;
            assistantBehaviour = settings.assistantBehaviour;
            assistantPatience = settings.assistantPatience;
        }*/

        numberOfPeople = LayTheTableSettings.Instance.numberOfPeople;
        numberOfLevel = LayTheTableSettings.Instance.numberOfLevel;
        targetsVisibility = LayTheTableSettings.Instance.targetsVisibility;
        assistantBehaviour = LayTheTableSettings.Instance.assistantBehaviour;
        assistantPatience = LayTheTableSettings.Instance.assistantPatience;
    }
}
