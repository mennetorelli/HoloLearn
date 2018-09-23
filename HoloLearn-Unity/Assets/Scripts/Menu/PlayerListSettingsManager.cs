using HoloToolkit.UI.Keyboard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            Destroy(playersList.GetChild(0).gameObject);
        }

        Vector3 offset = new Vector3();
        for (int i = 0; i < listOfPlayers.Count; i++)
        {
            Instantiate(PlayerEntry, playersList.transform.position + offset, PlayerEntry.transform.rotation, playersList);
            offset += new Vector3(0f, -0.07f, 0f);
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
        Instantiate(PlayerEntry, playersList.transform.position + new Vector3(0f, -0.07f * playersList.childCount, 0f), PlayerEntry.transform.rotation, playersList);
    }

    private void LoadSettings()
    {
        listOfPlayers = new List<string>() { "player1", "player2" };
        currentPlayer = PlayerListSettings.Instance.currentPlayer;
    }
}
