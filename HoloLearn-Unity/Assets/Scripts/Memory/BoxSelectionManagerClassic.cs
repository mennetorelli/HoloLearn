using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelectionManagerClassic : BoxSelectionManager
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
        if (manager.GetComponent<FindMeModeManager>().selectedElement != null)
        {
            if (manager.GetComponent<FindMeModeManager>().selectedElement.transform.GetChild(1).gameObject.name == transform.GetChild(1).gameObject.name)
            {
                VirtualAssistantManager.Instance.Jump();
                manager.GetComponent<FindMeModeManager>().selectedElement = null;
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
            manager.GetComponent<FindMeModeManager>().selectedElement = transform;
        }
        
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        Transform manager = GameObject.Find("MemoryManager").transform.GetChild(0);
        manager.GetComponent<FindMeModeManager>().selectedElement.transform.GetChild(0).gameObject.SetActive(true);
        manager.GetComponent<FindMeModeManager>().selectedElement.transform.GetChild(1).gameObject.SetActive(false);
        manager.GetComponent<FindMeModeManager>().selectedElement = null;
    }
}
