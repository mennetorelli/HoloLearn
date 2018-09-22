using HoloLearn;
using HoloToolkit.Examples.InteractiveElements;
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
    }
}
