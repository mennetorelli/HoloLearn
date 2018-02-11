using HoloLearn;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LayTheTableSettingsManager : Singleton<LayTheTableSettingsManager>
{
    private int level;
    private int people;
    private int targetsVisibility;
    private int assistant;
    private int patience;

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetPeople(int people)
    {
        this.people = people;
    }

    public void SetTargetsVisibility(int targetsVisibility)
    {
        this.targetsVisibility = targetsVisibility;
    }

    public void SetAssistant(int assistant)
    {
        this.assistant = assistant;
    }

    public void SetPatience(int patience)
    {
        this.patience = patience;
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

    public void RefreshAssistantButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("AssistantButtons").GetComponentsInChildren<InteractiveToggle>();
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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/layTheTableSettings.dat");

        LayTheTableSettings settings = new LayTheTableSettings();
        settings.level = level;
        settings.people = people;
        settings.targetsVisibility = targetsVisibility;
        settings.assistant = assistant;
        settings.patience = patience;

        bf.Serialize(file, settings);
        file.Close();
    }
}
