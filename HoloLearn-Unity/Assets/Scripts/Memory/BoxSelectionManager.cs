using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelectionManager : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleTap()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        MemoryManager manager = (MemoryManager)TaskManager.Instance;
        if (manager.selectedElement != null)
        {
            if (manager.selectedElement.transform.GetChild(1).gameObject.name == transform.GetChild(1).gameObject.name)
            {
                VirtualAssistantManager.Instance.Jump();
                manager.selectedElement = null;
                Counter.Instance.Decrement();
                Counter.Instance.Decrement();
            }
            else
            {
                VirtualAssistantManager.Instance.ShakeHead();
                StartCoroutine(Wait());
            }
            
        }
        else
        {
            manager.selectedElement = transform.gameObject;
        }
        
    }


    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        MemoryManager manager = (MemoryManager)TaskManager.Instance;
        manager.selectedElement.transform.GetChild(0).gameObject.SetActive(true);
        manager.selectedElement.transform.GetChild(1).gameObject.SetActive(false);
        manager.selectedElement = null;
    }
}
