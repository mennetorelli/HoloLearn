using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantChoiceManager : MonoBehaviour {

    public void Start()
    {
        RefreshMenu();
    }

    public void SetAssistantPresence(bool assistantPresence)
    {
        VirtualAssistantSettings.Instance.assistantPresence = assistantPresence;
        RefreshMenu();
    }


    public void LeftArrowClicked()
    {
        VirtualAssistantSettings.Instance.selectedAssistant--;
        RefreshMenu();
    }

    public void RightArrowClicked()
    {
        VirtualAssistantSettings.Instance.selectedAssistant++;
        RefreshMenu();
    }

    public void RefreshMenu()
    {
        InteractiveToggle assistantCheckBox = gameObject.transform.Find("AssistantCheckBox").GetComponent<InteractiveToggle>();
        if (VirtualAssistantSettings.Instance.assistantPresence)
        {
            assistantCheckBox.SetSelection(true);

            Animator[] assistants = GameObject.Find("VirtualAssistants").GetComponentsInChildren<Animator>(true);
            for (int i = 0; i < assistants.Length; i++)
            {
                assistants[i].gameObject.SetActive(false);
            }
            assistants[VirtualAssistantSettings.Instance.selectedAssistant].gameObject.SetActive(true);

            GameObject.Find("AssistantPanel").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("AssistantPanel").transform.GetChild(2).gameObject.SetActive(true);
            if (VirtualAssistantSettings.Instance.selectedAssistant == 0)
            {
                GameObject.Find("LeftArrow").SetActive(false);
            }
            if (VirtualAssistantSettings.Instance.selectedAssistant == GameObject.Find("VirtualAssistants").transform.childCount - 1)
            {
                GameObject.Find("RightArrow").SetActive(false);
            }
        }
        else
        {
            assistantCheckBox.SetSelection(false);
        }
    }
}
