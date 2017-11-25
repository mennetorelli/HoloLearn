using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SetSettings : MonoBehaviour
{
    public int level;
    public int people;

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
}
