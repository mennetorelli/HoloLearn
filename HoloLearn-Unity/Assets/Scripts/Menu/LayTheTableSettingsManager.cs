﻿using HoloLearn;
using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LayTheTableSettingsManager : MonoBehaviour
{  

    public void SetNumberOfLevel(int numberOfLevel)
    {
        LayTheTableSettings.Instance.numberOfLevel = numberOfLevel;
        RefreshMenu();
    }

    public void SetNumberOfPeople(int numberOfPeople)
    {
        LayTheTableSettings.Instance.numberOfPeople = numberOfPeople;
        RefreshMenu();
    }

    public void SetTargetsVisibility(int targetsVisibility)
    {
        LayTheTableSettings.Instance.targetsVisibility = targetsVisibility;
        RefreshMenu();
    }  


    public void RefreshMenu()
    {
        InteractiveToggle[] levelButtons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in levelButtons)
        {
            button.SetSelection(false);
        }
        levelButtons[LayTheTableSettings.Instance.numberOfLevel - 1].SetSelection(true);

        InteractiveToggle[] peopleButtons = gameObject.transform.transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in peopleButtons)
        {
            button.SetSelection(false);
        }
        peopleButtons[LayTheTableSettings.Instance.numberOfPeople - 1].SetSelection(true);

        InteractiveToggle targetCheckBox = gameObject.transform.Find("TargetCheckBox").GetComponent<InteractiveToggle>();
        if (LayTheTableSettings.Instance.targetsVisibility ==1)
        {
            targetCheckBox.SetSelection(true);
        }
        else
        {
            targetCheckBox.SetSelection(false);
        }

        
    }


    private void LoadSettings()
    {
    }

    public void SaveSettings()
    {
        SettingsFileManager.Instance.UpdateFile();
    }
}
