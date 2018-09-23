using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.UI.Keyboard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListSettingsManager : MonoBehaviour {

    private List<string> listOfPlayers;
    private int currentPlayer;

    public GameObject PlayerEntry;

    public void Start()
    {
        LoadSettings();
        RefreshMenu();
    }

    public void RefreshMenu()
    {
        Transform playersList = GameObject.Find("PlayersList").transform;

        for (int i=0; i < playersList.childCount; i++)
        {
            Destroy(playersList.GetChild(i).gameObject);
        }

        Vector3 offset = new Vector3();
        for (int i = 0; i < listOfPlayers.Count; i++)
        {
            GameObject entry = Instantiate(PlayerEntry, playersList.transform.position + offset, playersList.transform.rotation, playersList);
            offset += new Vector3(0f, -0.07f, 0f);

            entry.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = listOfPlayers.ElementAt(i);
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
                listOfPlayers.RemoveAt(i);
            }
        }
        RefreshMenu();
    }

    public void AddPlayerEntry()
    {
        string playerName = GameObject.Find("Keyboard").GetComponent<Keyboard>().InputField.text;
        listOfPlayers.Add(playerName);

        Transform playersList = GameObject.Find("PlayersList").transform;
        Vector3 offset = new Vector3(0f, -0.07f * playersList.childCount, 0f);
        GameObject entry = Instantiate(PlayerEntry, playersList.transform.position + offset, playersList.transform.rotation, playersList);

        string labelText = entry.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = playerName;
    }

    public void UpdatePlayerSelection(GameObject selectedButton)
    {
        InteractiveToggle[] buttons = gameObject.transform.Find("PlayersList").GetComponentsInChildren<InteractiveToggle>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetInstanceID() != selectedButton.GetInstanceID())
            {
                buttons[i].SetSelection(false);
                currentPlayer = i;
            }
        }
    }

    private void LoadSettings()
    {
        listOfPlayers = new List<string>() { "player1", "player2" };
        currentPlayer = PlayerListSettings.Instance.currentPlayer;
    }
}
