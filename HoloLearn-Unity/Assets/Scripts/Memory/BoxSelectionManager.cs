using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelectionManager : MonoBehaviour, IInputClickHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        transform.parent.GetChild(0).gameObject.SetActive(false);
        transform.parent.GetChild(1).gameObject.SetActive(true);

        MemoryManager manager = (MemoryManager)TaskManager.Instance;
        if (manager.selectedObject != null)
        {
            if (manager.selectedObject.name == transform.parent.GetChild(1).gameObject.name)
            {
                VirtualAssistantManager.Instance.Jump();
            }
            else
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
            manager.selectedObject = null;
        }
        else
        {
            manager.selectedObject = transform.parent.GetChild(1).gameObject;
            Debug.Log(manager.selectedObject);
        }
        
    }
}
