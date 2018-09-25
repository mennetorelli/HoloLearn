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

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void SetNumberOfLevel(int numberOfLevel)
    {
        LayTheTableSettings.Instance.numberOfLevel = numberOfLevel;
    }

    public void SetNumberOfPeople(int numberOfPeople)
    {
        LayTheTableSettings.Instance.numberOfPeople = numberOfPeople;
    }

    public void SetTargetsVisibility(int targetsVisibility)
    {
        LayTheTableSettings.Instance.targetsVisibility = targetsVisibility;
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
        levelButtons[LayTheTableSettings.Instance.numberOfLevel - 1].SetSelection(true);

        InteractiveToggle[] peopleButtons = gameObject.transform.transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
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
        /*XmlDocument xdoc = SettingsFileManager.Instance.LoadFile();
        XElement root = null;
        if (xdoc != null)
        {
            root = XElement.Load(new XmlNodeReader(xdoc));
        }*/


        XElement root =
            new XElement("Players",
                new XElement("Player",
                new XAttribute("PlayerName", "Menne"),
                    new XElement("LayTheTable",
                        new XAttribute("NumberOfLevel", 3),
                        new XAttribute("NumberOfPeople", 1),
                        new XAttribute("TargetsVisibility", 1)),
                    new XElement("GarbageCollection",
                        new XAttribute("numberOfBins", 2),
                        new XAttribute("NumberOfWaste", 5)),
                    new XElement("VirtualAssistant",
                        new XAttribute("SelectedAssistant", 0),
                        new XAttribute("AssistantBehaviour", 2),
                        new XAttribute("AssistantPatience", 5))),
                new XElement("Player",
                new XAttribute("PlayerName", "Player 2"),
                    new XElement("LayTheTable",
                        new XAttribute("NumberOfLevel", 1),
                        new XAttribute("NumberOfPeople", 3),
                        new XAttribute("TargetsVisibility", 0)),
                    new XElement("GarbageCollection",
                        new XAttribute("numberOfBins", 3),
                        new XAttribute("NumberOfWaste", 8)),
                    new XElement("VirtualAssistant",
                        new XAttribute("SelectedAssistant", 1),
                        new XAttribute("AssistantBehaviour", 1),
                        new XAttribute("AssistantPatience", 5))));

        Debug.Log(root);

        IEnumerable<XElement> settings =
            from item in root.Element("Player").Elements("LayTheTable")
            where ((string)root.Element("Player").Attribute("PlayerName") == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer))
            select item;

        Debug.Log(settings.Cast<XElement>().ToList().ElementAt(0));

        LayTheTableSettings.Instance.numberOfLevel = (int)settings.ElementAt(0).Attribute("NumberOfLevel");
        LayTheTableSettings.Instance.numberOfPeople = (int)settings.ElementAt(0).Attribute("NumberOfPeople");
        LayTheTableSettings.Instance.targetsVisibility = (int)settings.ElementAt(0).Attribute("TargetsVisibility");
    }

    public void SaveSettings()
    {
        SettingsFileManager.Instance.UpdateFile();
    }
}
