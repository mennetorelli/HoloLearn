using HoloLearn;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    }

    public void SetNumberOfBins(int numberOfBins)
    {
       this.numberOfBins = numberOfBins;
    }
    
    public void SetNumberOfWaste()
    {
        numberOfWaste = (int) wasteSlider.value;
        Debug.Log(numberOfWaste);
    }

    public void SetAssistantBehaviour(int assistantBehaviour)
    {
        this.assistantBehaviour = assistantBehaviour;
    }

    public void SetAssistantPatience(int assistantPatience)
    {
        this.assistantPatience = assistantPatience;
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
    /* public void RefreshWasteButtons(GameObject selectedButton)
     {
         InteractiveToggle[] buttons = gameObject.transform.Find("WasteButtons").GetComponentsInChildren<InteractiveToggle>();
         foreach (InteractiveToggle button in buttons)
         {
             if (button.GetInstanceID() != selectedButton.GetInstanceID())
             {
                 button.SetSelection(false);
             }
         }
     }*/

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

    public void MakeSliderAppear(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();

        foreach (InteractiveToggle button in buttons)
        {
            if (button.CompareTag("Dynamic"))
            {
                gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("PatientTime").gameObject.SetActive(true);
            }
        }
    }

    public void MakeSliderDisAppear(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();

        foreach (InteractiveToggle button in buttons)
        {
            if (button.CompareTag("Dynamic"))
            {
                gameObject.transform.Find("VirtualAssistantMenu").transform.Find("RestDisappear").transform.Find("PatientTime").gameObject.SetActive(false);
            }
        }
    }


    private void RefreshAllButtons()
    {
        InteractiveToggle[] binsButtons = gameObject.transform.Find("SettingsGC").transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        binsButtons[numberOfBins - 1].SetSelection(true);

        InteractiveToggle[] wasteButtons = gameObject.transform.Find("SettingsGC").transform.Find("PeopleButtons").GetComponentsInChildren<InteractiveToggle>();
        binsButtons[numberOfBins - 1].SetSelection(true);

        InteractiveToggle[] assistantBehaviourButtons = gameObject.transform.Find("VirtualAssistantGC").transform.Find("RestDisappear").transform.Find("ModeButtons").GetComponentsInChildren<InteractiveToggle>();
        assistantBehaviourButtons[assistantBehaviour - 1].SetSelection(true);
    }


    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GarbageCollectionSettings.dat");

        GarbageCollectionSettings settings = new GarbageCollectionSettings();
        settings.numberOfBins = numberOfBins;
        settings.numberOfWaste = numberOfWaste;
        settings.asistantBehaviour = assistantBehaviour;
        settings.assistantPatience = assistantPatience;

        bf.Serialize(file, settings);
        file.Close();
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/layTheTableSettings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GarbageCollectionSettings.dat", FileMode.Open);

            GarbageCollectionSettings settings = (GarbageCollectionSettings)bf.Deserialize(file);
            file.Close();

            numberOfBins = settings.numberOfBins;
            numberOfWaste = settings.numberOfWaste;
            assistantBehaviour = settings.asistantBehaviour;
            assistantPatience = settings.assistantPatience;
        }
    }
}
