using HoloLearn;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GarbageCollectionSettingsManager : Singleton<GarbageCollectionSettingsManager>
{
    private int numberOfBins;
    private int numberOfWaste;
    private int assistantBehaviour;
    private int assistantPatience;
    public Slider wasteSlider;

    public void Start()
    {
        LoadSettings();
        RefreshGeneralMenu();
    }

    public void SetNumberOfBins(int numberOfBins)
    {
       this.numberOfBins = numberOfBins;
    }
    
    public void SetNumberOfWaste()
    {
       HoloToolkit.Examples.InteractiveElements.SliderGestureControl slider = gameObject.transform.Find("SettingsGC").transform.Find("Slider").GetComponentInChildren<HoloToolkit.Examples.InteractiveElements.SliderGestureControl>();
       numberOfWaste = (int)slider.SliderValue;
    }

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        this.assistantBehaviour = assistantBehaviour;
    }

    public void SetAssistantPatience(int assistantPatience)
    {
        HoloToolkit.Examples.InteractiveElements.SliderGestureControl slider = gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<HoloToolkit.Examples.InteractiveElements.SliderGestureControl>();
        assistantPatience = Convert.ToInt32(slider.SliderValue);
    }


    public void RefreshBinsButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("SettingsGC").transform.Find("BinsButtons").GetComponentsInChildren<InteractiveToggle>();
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
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();

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
        InteractiveToggle[] binsButtons = gameObject.transform.Find("SettingsGC").transform.Find("BinsButtons").GetComponentsInChildren<InteractiveToggle>();
        binsButtons[numberOfBins - 1].SetSelection(true);

        GameObject.Find("SettingsGC").transform.Find("Slider").GetComponentInChildren<HoloToolkit.Examples.InteractiveElements.SliderGestureControl>().SetSliderValue(assistantPatience);
    }


    public void RefreshAssistantMenu()
    {
        HoloToolkit.Examples.InteractiveElements.InteractiveToggle assistantCheckBox = gameObject.transform.Find("VirtualAssistantGC").transform.Find("CheckBox").GetComponent<HoloToolkit.Examples.InteractiveElements.InteractiveToggle>();
        if (assistantBehaviour != 0)
        {
            assistantCheckBox.SetSelection(true);
            gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").gameObject.SetActive(true);
            InteractiveToggle[] assistantBehaviourButtons = gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
            assistantBehaviourButtons[assistantBehaviour - 1].SetSelection(true);
            if (assistantBehaviour == 2)
            {
                GameObject.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("PatientTime").GetComponentInChildren<HoloToolkit.Examples.InteractiveElements.SliderGestureControl>().SetSliderValue(assistantPatience);
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
        FileStream file = File.Create(Application.persistentDataPath + "/garbageCollectionSettings.dat");

        GarbageCollectionSettings settings = new GarbageCollectionSettings();
        settings.numberOfBins = numberOfBins;
        settings.numberOfWaste = numberOfWaste;
        settings.asistantBehaviour = assistantBehaviour;
        settings.assistantPatience = assistantPatience;

        bf.Serialize(file, settings);
        file.Close();*/

        GarbageCollectionSettings.Instance.numberOfBins = numberOfBins;
        GarbageCollectionSettings.Instance.numberOfWaste = numberOfWaste;
        GarbageCollectionSettings.Instance.asistantBehaviour = assistantBehaviour;
        GarbageCollectionSettings.Instance.assistantPatience = assistantPatience;
    }

    private void LoadSettings()
    {
        /*if (File.Exists(Application.persistentDataPath + "/garbageCollectionSettings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/garbageCollectionSettings.dat", FileMode.Open);

            GarbageCollectionSettings settings = (GarbageCollectionSettings)bf.Deserialize(file);
            file.Close();

            numberOfBins = settings.numberOfBins;
            numberOfWaste = settings.numberOfWaste;
            assistantBehaviour = settings.asistantBehaviour;
            assistantPatience = settings.assistantPatience;
        }*/

        numberOfBins = GarbageCollectionSettings.Instance.numberOfBins;
        numberOfWaste = GarbageCollectionSettings.Instance.numberOfWaste;
        assistantBehaviour = GarbageCollectionSettings.Instance.asistantBehaviour;
        assistantPatience = GarbageCollectionSettings.Instance.assistantPatience;
    }
}
