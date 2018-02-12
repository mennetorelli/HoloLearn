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
    private int bins;
    private int waste;
    private int assistant;
    private int patience;
    private int visibility;
    public Slider wasteSlider;
    public int mode;

    public void Start()
    {
        LoadSettings();
    }

    public void SetBins(int bins)
    {
       this.bins = bins;
    }
    
    public void SetWaste(int waste)
    {
        this.waste = (int)wasteSlider.value;
    }

    public void SetAssistant(int assistant)
    {
        this.assistant = assistant;
    }

    public void SetPatience(int patience)
    {
        this.patience = patience;
    }

    public void SetMode(int mode)
    {
        this.mode = mode;
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
        FileStream file = File.Create(Application.persistentDataPath + "/GarbageCollectionSettings.dat");

        GarbageCollectionSettings settings = new GarbageCollectionSettings();
        settings.bins = bins;
        settings.waste = waste;
        settings.assistant = assistant;
        settings.patience = patience;

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

            bins = settings.bins;
            waste = settings.waste;
            assistant = settings.assistant;
            patience = settings.patience;
        }
    }
}
