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

        MemoryManager manager = (MemoryManager)TaskManager.Instance;
        if (manager.selectedElement.transform.GetChild(1).gameObject.name == transform.GetChild(1).gameObject.name)
        {
            VirtualAssistantManager.Instance.Jump();
            Counter.Instance.Decrement();
        }
        else
        {
            VirtualAssistantManager.Instance.ShakeHead();
        }

    }
}
