using HoloLearn;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GarbageCollectionSettingsManager : Singleton<GarbageCollectionSettingsManager>
{
    private int bins;
    private int waste;
    private int assistant;
    private int patience;

    public void SetBins(int bins)
    {
       this.bins = bins;
    }

    public void SetWaste(int waste)
    {
        this.waste = waste;
    }

    public void SetAssistant(int assistant)
    {
        this.assistant = assistant;
    }

    public void SetPatience(int patience)
    {
        this.patience = patience;
    }

    public void RefreshBinsButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("BinsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
    }
    public void RefreshWasteButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("WasteButtons").GetComponentsInChildren<InteractiveToggle>();
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
        FileStream file = File.Create(Application.persistentDataPath + "/GarbageCollectionSettings.dat");

        GarbageCollectionSettings settings = new GarbageCollectionSettings();
        settings.bins = bins;
        settings.waste = waste;
        settings.assistant = assistant;
        settings.patience = patience;

        bf.Serialize(file, settings);
        file.Close();
    }
}
