using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSettingsSL : Singleton<SetSettingsSL>
{

    public int collectors = 2;
    public int objects = 5;

   
    public void setCollectorsThree()
    {
        collectors = 3;
    }
    public void setCollectorsTwo()
    {
        collectors = 2;
    }
    public void setObjectsFive()
    {
        objects = 5;
    }
    public void setObjectsSeven()
    {
        objects = 7;
    }
    public void setObjectsTen()
    {
        objects = 10;
    }
}
