using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : Singleton<Counter> {

    private int count;
    public GameObject endMenu;



	// Use this for initialization
	void Start ()
    {
        
	}

    public void Decrement()
    {
        count--;
        if (count == 0)
        {
            VirtualAssistantManager.Instance.GetComponent<Animator>().SetTrigger("EndGame");
        }
        //Debug.Log(count);
    }

    public void InitializeCounter(Transform remainingObjects)
    {
        count = remainingObjects.GetComponentsInChildren<Rigidbody>().Length;
        //Debug.Log(count);
    }

   
     
        

}
