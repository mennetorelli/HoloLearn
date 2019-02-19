using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteractionHandler : MonoBehaviour, ISourceStateHandler, ISpeechHandler, IInputClickHandler
{

    private bool readyToPlay;
    private int inputSources;

	// Use this for initialization
	void Start ()
    {
        readyToPlay = false;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}


    public void OnSourceDetected(SourceStateEventData eventData)
    {
        inputSources += 1;
        Debug.Log(inputSources);
        if (inputSources == 2)
        {
            SetMenuVisible();
            Debug.Log(inputSources);
        }
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        inputSources -= 1;
        Debug.Log(inputSources);
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
        inputSources = 0;

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        StartCoroutine(Wait());
    }

    public void StartPlay()
    {
        if (readyToPlay)
        {
            TaskManager.Instance.GenerateObjectsInWorld();
            readyToPlay = false;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        StartPlay();
    }


}
