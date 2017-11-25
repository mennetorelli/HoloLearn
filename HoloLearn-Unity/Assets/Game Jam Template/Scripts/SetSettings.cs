using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SetSettings : Singleton<SetSettings>
{
    public int level;
    public int people;
    public int collectors;
    public int objects;

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
