using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelectionManagerFindMe : BoxSelectionManager
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void HandleTap()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        Transform manager = GameObject.Find("MemoryManager").transform.GetChild(0);
        manager.GetComponent<FindMeModeManager>().selectedElement = transform;

        manager.GetChild(1).gameObject.SetActive(true);
    }


   

}
