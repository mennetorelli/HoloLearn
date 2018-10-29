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

    public void SetPlayerGender(int playerGender)
    {
        DressUpSettings.Instance.playerGender = playerGender;
        RefreshMenu();
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
        InteractiveToggle[] genderButtons = gameObject.transform.Find("GenderButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in genderButtons)
        {
            button.SetSelection(false);
        }
        genderButtons[DressUpSettings.Instance.playerGender].SetSelection(true);

        InteractiveToggle[] binsButtons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in binsButtons)
        {
            button.SetSelection(false);
        }
        binsButtons[DressUpSettings.Instance.numberOfLevel - 1].SetSelection(true);

        transform.Find("Slider").GetComponentInChildren<SliderGestureControl>().SetSliderValue(DressUpSettings.Instance.numberOfClothes);
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("DressUpSettings",
                new XAttribute("PlayerGender", DressUpSettings.Instance.playerGender),
                new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfLevel),
                new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfClothes));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
