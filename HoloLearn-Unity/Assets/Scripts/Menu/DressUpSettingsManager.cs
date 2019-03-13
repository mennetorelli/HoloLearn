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

public class DressUpSettingsManager : MonoBehaviour
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

    public void SetNumberOfLevel(int numberOfLevel)
    {
        DressUpSettings.Instance.numberOfLevel = numberOfLevel;
        RefreshMenu();
    }

    public void SetNumberOfClothes()
    {
        SliderGestureControl slider = gameObject.transform.Find("Slider").GetComponentInChildren<SliderGestureControl>();
        DressUpSettings.Instance.numberOfClothes = Convert.ToInt32(slider.SliderValue) + 3;
    }


    public void RefreshMenu()
    {
        InteractiveToggle[] levelsButtons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in levelsButtons)
        {
            button.SetSelection(false);
        }
        levelsButtons[DressUpSettings.Instance.numberOfLevel - 1].SetSelection(true);

        transform.Find("Slider").GetComponentInChildren<SliderGestureControl>().SetSliderValue(DressUpSettings.Instance.numberOfClothes);
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("DressUpSettings",
                new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfLevel),
                new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfClothes));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
