using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteractionHandler : MonoBehaviour, ISourceStateHandler, ISpeechHandler, IInputClickHandler
{

    private bool readyToPlay;
    private bool playing;
    private bool handRecognized;

	// Use this for initialization
	void Start ()
    {
        readyToPlay = false;
        playing = false;
        handRecognized = false;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}


    public void OnSourceDetected(SourceStateEventData eventData)
    {
        if (playing)
        {
            if (!handRecognized)
            {
                handRecognized = true;
                StartCoroutine(WaitForSecondSource());
            }
            else
            {
                SetMenuVisible();
            }
        }
    }

    private IEnumerator WaitForSecondSource()
    {
        yield return new WaitForSeconds(0.1f);
        handRecognized = false;
    }


    public void OnSourceLost(SourceStateEventData eventData)
    {
        handRecognized = false;
    }


    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        //throw new System.NotImplementedException();
    }


    public void SetMenuVisible()
    {
        playing = false;
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void ScanningComplete()
    {
        readyToPlay = true;

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        StartCoroutine(Wait());
    }

    public void StartPlay()
    {
        if (readyToPlay)
        {
            playing = true;
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
        if (readyToPlay)
        {
            StartPlay();
        }
    }


}
