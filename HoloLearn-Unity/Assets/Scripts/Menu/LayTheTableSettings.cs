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

    /*public void UpdateColor()
    {
        Transform buttons = gameObject.transform.Find("Levels");
        Debug.Log(gameObject.name);
        Debug.Log(buttons.GetChild(0));
        buttons.GetChild(0).GetComponent<Material>().color = new Color(0, 0, 1);

    }*/
}
