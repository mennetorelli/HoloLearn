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
        if (manager.selectedElement != null)
        {
            if (manager.selectedElement.transform.GetChild(1).gameObject.name == transform.parent.GetChild(1).gameObject.name)
            {
                VirtualAssistantManager.Instance.Jump();
            }
            else
            {
                VirtualAssistantManager.Instance.ShakeHead();
                Debug.Log("da aspettare");
                StartCoroutine(Wait());
                transform.parent.GetChild(0).gameObject.SetActive(true);
                transform.parent.GetChild(1).gameObject.SetActive(false);
                manager.selectedElement.transform.GetChild(0).gameObject.SetActive(true);
                manager.selectedElement.transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("ritornato");
            }
            manager.selectedElement = null;
        }
        else
        {
            manager.selectedElement = transform.parent.gameObject;
        }
        
    }


    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
