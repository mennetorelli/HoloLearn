using HoloLearn;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LayTheTableSettingsManager : Singleton<LayTheTableSettingsManager>
{
    private int numberOfLevel;
    private int numberOfPeople;
    private int targetsVisibility;
    private int assistantBehaviour;
    private int assistantPatience;
    

    public void Start()
    {
        LoadSettings();
    }

    public void SetLevel(int numberOfLevel)
    {
        this.numberOfLevel = numberOfLevel;
    }

    public void SetPeople(int numberOfPeople)
    {
        this.numberOfPeople = numberOfPeople;
    }

    public void SetVisibility(int targetsVisibility)
    {
        this.targetsVisibility = targetsVisibility;
    }

    public void SetAssistant(int assistantBehaviour)
    {
        this.assistantBehaviour = assistantBehaviour;
    }

    public void SetPatience(int assistantPatience)
    {
        this.assistantPatience = assistantPatience;
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
      //  InteractiveToggle[] buttons = gameObject.transform.Find("SettingsLTT").GetComponentsInChildren<InteractiveToggle>();
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
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantMenu").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
        
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }

    public void MakeSliderAppear(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantMenu").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();

        foreach (InteractiveToggle button in buttons)
        {
            if (button.CompareTag("Dynamic"))
            {
                gameObject.transform.Find("VirtualAssistantMenu").transform.Find("RestDisappear").transform.Find("PatientTime").gameObject.SetActive(true);
            }
        }    
    }

    public void MakeSliderDisAppear(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantMenu").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();

        foreach (InteractiveToggle button in buttons)
        {
            if (button.CompareTag("Dynamic"))
            {
                gameObject.transform.Find("VirtualAssistantMenu").transform.Find("RestDisappear").transform.Find("PatientTime").gameObject.SetActive(false);
            }
        }
    }


    public void SaveSettings()
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/layTheTableSettings.dat");

        LayTheTableSettings settings = new LayTheTableSettings();
        settings.numberOfLevel = numberOfLevel;
        settings.numberOfPeople = numberOfPeople;
        settings.targetsVisibility = targetsVisibility;
        settings.assistantBehaviour = assistantBehaviour;
        settings.assistantPatience = assistantPatience;

        bf.Serialize(file, settings);
        file.Close();
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/layTheTableSettings.dat"))
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
        }
    }
}
