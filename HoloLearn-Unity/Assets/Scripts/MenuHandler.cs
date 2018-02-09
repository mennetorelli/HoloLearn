using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour, ISpeechHandler
{

    private bool readyToPlay;

	// Use this for initialization
	void Start ()
    {
        readyToPlay = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        //throw new System.NotImplementedException();
    }


    public void SetMenuVisible()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void ScanningComplete()
    {
        readyToPlay = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void StartPlay()
    {
        if (readyToPlay)
        {
            TaskManager.Instance.GenerateObjectsInWorld();
            transform.GetChild(1).gameObject.SetActive(false);
            readyToPlay = false;
        }
    }

}
