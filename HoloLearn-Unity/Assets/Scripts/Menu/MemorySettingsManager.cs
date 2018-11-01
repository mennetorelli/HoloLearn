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

public class MemorySettingsManager : MonoBehaviour
{

    public void SetPlayMode(int playMode)
    {
        MemorySettings.Instance.playMode = playMode;
        RefreshMenu();
    }

    public void SetNumberOfBoxes(int numberOfBoxes)
    {
        MemorySettings.Instance.numberOfBoxes = numberOfBoxes;
        RefreshMenu();
    }

    public void SetWaitingTime()
    {
        SliderGestureControl slider = gameObject.transform.Find("Slider").GetComponentInChildren<SliderGestureControl>();
        MemorySettings.Instance.waitingTime = Convert.ToInt32(slider.SliderValue) + 3;
    }


    public void RefreshMenu()
    {
        InteractiveToggle[] playModeButtons = gameObject.transform.Find("PlayModeButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in playModeButtons)
        {
            button.SetSelection(false);
        }
        playModeButtons[MemorySettings.Instance.playMode].SetSelection(true);

        InteractiveToggle[] boxesButtons = gameObject.transform.Find("BoxesButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in boxesButtons)
        {
            button.SetSelection(false);
        }
        boxesButtons[(MemorySettings.Instance.numberOfBoxes - 4) / 2 - 1].SetSelection(true);

        transform.Find("Slider").GetComponentInChildren<SliderGestureControl>().SetSliderValue(DressUpSettings.Instance.numberOfClothes);
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("MemorySettings",
                new XAttribute("PlayMode", MemorySettings.Instance.playMode),
                new XAttribute("NumberOfBoxes", MemorySettings.Instance.numberOfBoxes),
                new XAttribute("WaitingTime", MemorySettings.Instance.waitingTime));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
