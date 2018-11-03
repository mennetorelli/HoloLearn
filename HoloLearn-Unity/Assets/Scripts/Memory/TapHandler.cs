using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHandler : MonoBehaviour, IInputClickHandler
{
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject.Find("MemoryManager").transform.GetChild(0).GetComponent<PlayModeManager>().HandleTap(transform.parent);
    }
}
