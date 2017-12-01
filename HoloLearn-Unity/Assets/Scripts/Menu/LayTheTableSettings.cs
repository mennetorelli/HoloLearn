using HoloLearn;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayTheTableSettings : Singleton<LayTheTableSettings>
{
    public int level = 1;
    public int people = 1;
   
 public void setLevelToEasy()
    {
        level = 1;
    }

    public void setLevelToHard()
    {
        level = 2;
    }
    public void setPeopleOne()
    {
        people = 1;
    }
    public void setPeopleTwo()
    {
        people = 2;
    }
    public void setPeopleThree()
    {
        people = 3;
    }

    public void RefreshLevelsButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
        selectedButton.GetComponent<InteractiveToggle>().SetSelection(true);
    }

    public void RefreshPeopleButtons(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<InteractiveToggle>();
        foreach (InteractiveToggle button in buttons)
        {
            if (button.GetInstanceID() != selectedButton.GetInstanceID())
            {
                button.SetSelection(false);
            }
        }
        selectedButton.GetComponent<InteractiveToggle>().SetSelection(true);
    }
}
