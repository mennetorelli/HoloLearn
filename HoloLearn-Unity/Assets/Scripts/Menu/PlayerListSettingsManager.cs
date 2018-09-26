using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.UI.Keyboard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListSettingsManager : MonoBehaviour {

    public GameObject PlayerEntry;

    public void RefreshMenu()
    {
        Transform playersList = GameObject.Find("PlayersList").transform;

        for (int i=0; i < playersList.childCount; i++)
        {
            Destroy(playersList.GetChild(i).gameObject);
        }

        Vector3 offset = new Vector3();
        for (int i = 0; i < PlayerListSettings.Instance.listOfPlayers.Count; i++)
        {
            GameObject entry = Instantiate(PlayerEntry, playersList.transform.position + offset, playersList.transform.rotation, playersList);
            offset += new Vector3(0f, -0.07f, 0f);

            entry.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = PlayerListSettings.Instance.listOfPlayers.ElementAt(i);
        }

    }

    public void DeletePlayerEntry(GameObject caller)
    {
        Transform playersList = GameObject.Find("PlayersList").transform;
        for (int i = 0; i < playersList.childCount; i++)
        {
            if (playersList.GetChild(i).gameObject.GetInstanceID() == caller.GetInstanceID())
            {
                Destroy(playersList.GetChild(i).gameObject.gameObject);
                PlayerListSettings.Instance.listOfPlayers.RemoveAt(i);
            }
        }
        RefreshMenu();
    }

    public void AddPlayerEntry()
    {

        string playerName = GameObject.Find("Keyboard").GetComponent<Keyboard>().InputField.text;
        PlayerListSettings.Instance.listOfPlayers.Add(playerName);

        Transform playersList = GameObject.Find("PlayersList").transform;
        Vector3 offset = new Vector3(0f, -0.07f * playersList.childCount, 0f);
        GameObject entry = Instantiate(PlayerEntry, playersList.transform.position + offset, playersList.transform.rotation, playersList);

        string labelText = entry.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = playerName;

        PlayerListSettings.Instance.currentPlayer = PlayerListSettings.Instance.listOfPlayers.IndexOf(playerName);

        LayTheTableSettings.Instance.numberOfLevel = 1;
        LayTheTableSettings.Instance.numberOfPeople = 1;
        LayTheTableSettings.Instance.targetsVisibility = 1;
        GarbageCollectionSettings.Instance.numberOfBins = 2;
        GarbageCollectionSettings.Instance.numberOfWaste = 5;
        VirtualAssistantChoice.Instance.assistantPresence = 1;
        VirtualAssistantChoice.Instance.selectedAssistant = 0;
        VirtualAssistantSettings.Instance.assistantBehaviour = 1;
        VirtualAssistantSettings.Instance.assistantPatience = 5;

        XElement root = SettingsFileManager.Instance.GetXML();

        XElement newPlayer =
            new XElement("Player",
            new XAttribute("PlayerName", PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)),
                new XElement("LayTheTableSettings",
                    new XAttribute("NumberOfLevel", LayTheTableSettings.Instance.numberOfLevel),
                    new XAttribute("NumberOfPeople", LayTheTableSettings.Instance.numberOfPeople),
                    new XAttribute("TargetsVisibility", LayTheTableSettings.Instance.targetsVisibility)),
                new XElement("GarbageCollectionSettings",
                    new XAttribute("NumberOfBins", GarbageCollectionSettings.Instance.numberOfBins),
                    new XAttribute("NumberOfWaste", GarbageCollectionSettings.Instance.numberOfWaste)),
                new XElement("VirtualAssistantChoice",
                    new XAttribute("AssistantPresence", VirtualAssistantChoice.Instance.assistantPresence),
                    new XAttribute("SelectedAssistant", VirtualAssistantChoice.Instance.selectedAssistant)),
                new XElement("VirtualAssistantSettings",
                    new XAttribute("AssistantBehaviour", VirtualAssistantSettings.Instance.assistantBehaviour),
                    new XAttribute("AssistantPatience", VirtualAssistantSettings.Instance.assistantPatience)));

        Debug.Log(root);
        root.Add(newPlayer);
        Debug.Log(root);
    }

    public void UpdatePlayerSelection(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("PlayersList").GetComponentsInChildren<InteractiveToggle>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetInstanceID() != selectedButton.GetInstanceID())
            {
                buttons[i].SetSelection(false);
                PlayerListSettings.Instance.currentPlayer = i;
            }
        }

        XElement root = SettingsFileManager.Instance.GetXML();

        IEnumerable<XElement> layTheTableSettings =
                from item in root.Elements("Player")
                where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
                select item.Element("LayTheTableSettings");

        LayTheTableSettings.Instance.numberOfLevel = (int)layTheTableSettings.ElementAt(0).Attribute("NumberOfLevel");
        LayTheTableSettings.Instance.numberOfPeople = (int)layTheTableSettings.ElementAt(0).Attribute("NumberOfPeople");
        LayTheTableSettings.Instance.targetsVisibility = (int)layTheTableSettings.ElementAt(0).Attribute("TargetsVisibility");


        IEnumerable<XElement> garbageCollectionSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
            select item.Element("GarbageCollectionSettings");

        GarbageCollectionSettings.Instance.numberOfBins = (int)garbageCollectionSettings.ElementAt(0).Attribute("NumberOfBins");
        GarbageCollectionSettings.Instance.numberOfWaste = (int)garbageCollectionSettings.ElementAt(0).Attribute("NumberOfWaste");


        IEnumerable<XElement> virtualAssistantChoice =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
            select item.Element("VirtualAssistantChoice");

        VirtualAssistantChoice.Instance.assistantPresence = (int)virtualAssistantChoice.ElementAt(0).Attribute("AssistantPresence");
        VirtualAssistantChoice.Instance.selectedAssistant = (int)virtualAssistantChoice.ElementAt(0).Attribute("SelectedAssistant");


        IEnumerable<XElement> virtualAssistantSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
            select item.Element("VirtualAssistantSettings");

        VirtualAssistantSettings.Instance.assistantBehaviour = (int)virtualAssistantSettings.ElementAt(0).Attribute("AssistantBehaviour");
        VirtualAssistantSettings.Instance.assistantPatience = (int)virtualAssistantSettings.ElementAt(0).Attribute("AssistantPatience");
    }


    public void SaveSettings()
    {
        XElement root = SettingsFileManager.Instance.GetXML();

        IEnumerable<XElement> players =
                from item in root.Elements("Player")
                select item;

        
        SettingsFileManager.Instance.UpdateFile(root);
    }
}
