using HoloLearn;
using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GarbageCollectionSettingsManager : MonoBehaviour
{
    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void SetNumberOfBins(int numberOfBins)
    {
       GarbageCollectionSettings.Instance.numberOfBins = numberOfBins;
    }
    
    public void SetNumberOfWaste()
    {
       SliderGestureControl slider = gameObject.transform.Find("Slider").GetComponentInChildren<SliderGestureControl>();
       GarbageCollectionSettings.Instance.numberOfWaste = Convert.ToInt32(slider.SliderValue) + 3;
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
        binsButtons[GarbageCollectionSettings.Instance.numberOfBins - 1].SetSelection(true);

        transform.Find("Slider").GetComponentInChildren<SliderGestureControl>().SetSliderValue(GarbageCollectionSettings.Instance.numberOfWaste);
    }


    private void LoadSettings()
    {

    }

    public void SaveSettings()
    {

    }
}
