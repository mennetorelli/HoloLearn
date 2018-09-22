using HoloLearn;
using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GarbageCollectionSettingsManager : MonoBehaviour
{
    private int numberOfBins;
    private int numberOfWaste;

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void SetNumberOfBins(int numberOfBins)
    {
       this.numberOfBins = numberOfBins;
    }
    
    public void SetNumberOfWaste()
    {
       SliderGestureControl slider = gameObject.transform.Find("Slider").GetComponentInChildren<SliderGestureControl>();
       numberOfWaste = Convert.ToInt32(slider.SliderValue) + 3;
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

 
    public void RefreshMenu()
    {
        InteractiveToggle[] binsButtons = gameObject.transform.Find("BinsButtons").GetComponentsInChildren<InteractiveToggle>();
        binsButtons[numberOfBins - 1].SetSelection(true);

        transform.Find("Slider").GetComponentInChildren<SliderGestureControl>().SetSliderValue(numberOfWaste);
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
    }
}
