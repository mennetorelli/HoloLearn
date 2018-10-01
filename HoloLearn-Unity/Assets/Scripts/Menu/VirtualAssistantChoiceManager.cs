using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class VirtualAssistantChoiceManager : MonoBehaviour
{

    public void SetAssistantPresence(int assistantPresence)
    {
        VirtualAssistantChoice.Instance.assistantPresence = assistantPresence;
        RefreshMenu();
    }

    public void LeftArrowClicked()
    {
        VirtualAssistantChoice.Instance.selectedAssistant--;
        RefreshMenu();
    }

    public void RightArrowClicked()
    {
        VirtualAssistantChoice.Instance.selectedAssistant++;
        RefreshMenu();
    }

    public void RefreshMenu()
    {
        InteractiveToggle assistantCheckBox = gameObject.transform.Find("AssistantCheckBox").GetComponent<InteractiveToggle>();
        if (VirtualAssistantChoice.Instance.assistantPresence == 1)
        {
            assistantCheckBox.SetSelection(true);

            Animator[] assistants = GameObject.Find("VirtualAssistants").GetComponentsInChildren<Animator>(true);
            for (int i = 0; i < assistants.Length; i++)
            {
                assistants[i].gameObject.SetActive(false);
            }
            assistants[VirtualAssistantChoice.Instance.selectedAssistant].gameObject.SetActive(true);

            GameObject.Find("AssistantPanel").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("AssistantPanel").transform.GetChild(2).gameObject.SetActive(true);
            if (VirtualAssistantChoice.Instance.selectedAssistant == 0)
            {
                GameObject.Find("LeftArrow").SetActive(false);
            }
            if (VirtualAssistantChoice.Instance.selectedAssistant == GameObject.Find("VirtualAssistants").transform.childCount - 1)
            {
                GameObject.Find("RightArrow").SetActive(false);
            }
        }
        else
        {
            assistantCheckBox.SetSelection(false);
        }
    }


    public void SaveSettings()
    {
        XElement root = SettingsFileManager.Instance.LoadFile();

        IEnumerable<XElement> oldSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
            select item.Element("VirtualAssistantChoice");

        XElement newSettings =
            new XElement("VirtualAssistantChoice",
                new XAttribute("AssistantPresence", VirtualAssistantChoice.Instance.assistantPresence),
                new XAttribute("SelectedAssistant", VirtualAssistantChoice.Instance.selectedAssistant));

        oldSettings.ElementAt(0).ReplaceWith(newSettings);
        SettingsFileManager.Instance.UpdateFile(root);
    }
}
