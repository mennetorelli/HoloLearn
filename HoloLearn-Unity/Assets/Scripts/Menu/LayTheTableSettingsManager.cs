using HoloLearn;
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
    private int numberOfLevel;
    private int numberOfPeople;
    private int targetsVisibility;
    

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
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

    
    public void RefreshLevelsButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
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
        InteractiveToggle[] buttons = gameObject.transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }

    


    public void RefreshMenu()
    {
        InteractiveToggle[] levelButtons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        levelButtons[numberOfLevel - 1].SetSelection(true);

        InteractiveToggle[] peopleButtons = gameObject.transform.transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
        peopleButtons[numberOfPeople - 1].SetSelection(true);

        InteractiveToggle targetCheckBox = gameObject.transform.Find("TargetCheckBox").GetComponent<InteractiveToggle>();
        if (targetsVisibility==1)
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
        XmlDocument xdoc = SettingsFileManager.Instance.LoadFile();
        XElement root = XElement.Load(new XmlNodeReader(xdoc));
        IEnumerable<int> settings =
            from item in root.Elements("LayTheTable")
            select (int)item;

        numberOfPeople = LayTheTableSettings.Instance.numberOfPeople;
        numberOfLevel = LayTheTableSettings.Instance.numberOfLevel;
        targetsVisibility = LayTheTableSettings.Instance.targetsVisibility;
    }

    public void SaveSettings()
    {
        LayTheTableSettings.Instance.numberOfLevel = numberOfLevel;
        LayTheTableSettings.Instance.numberOfPeople = numberOfPeople;
        LayTheTableSettings.Instance.targetsVisibility = targetsVisibility;

        SettingsFileManager.Instance.UpdateFile();
    }
}
