using HoloLearn;
using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class GarbageCollectionSettingsManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfBins(int numberOfBins)
    {
       GarbageCollectionSettings.Instance.numberOfBins = numberOfBins;
       RefreshMenu();
    }
    
    public void SetNumberOfWaste()
    {
       SliderGestureControl slider = gameObject.transform.Find("Slider").GetComponentInChildren<SliderGestureControl>();
       GarbageCollectionSettings.Instance.numberOfWaste = Convert.ToInt32(slider.SliderValue) + 3;
    }

 
    public void RefreshMenu()
    {
        InteractiveToggle[] binsButtons = gameObject.transform.Find("BinsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in binsButtons)
        {
            button.SetSelection(false);
        }
        binsButtons[GarbageCollectionSettings.Instance.numberOfBins - 1].SetSelection(true);

        transform.Find("Slider").GetComponentInChildren<SliderGestureControl>().SetSliderValue(GarbageCollectionSettings.Instance.numberOfWaste);
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("GarbageCollectionSettings",
                new XAttribute("NumberOfBins", GarbageCollectionSettings.Instance.numberOfBins),
                new XAttribute("NumberOfWaste", GarbageCollectionSettings.Instance.numberOfWaste));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
