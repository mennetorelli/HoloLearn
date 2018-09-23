using System.Collections;
using System.Collections.Generic;
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
        GameObject playersList = GameObject.Find("PlayersList");
        if (playersList.transform.childCount != 0)
        {
            GameObject[] playersToRefresh = playersList.GetComponents<GameObject>();
            foreach (GameObject elem in playersToRefresh)
            {
                Destroy(elem);
            }
        }

        Vector3 offset = new Vector3();
        for (int i = 0; i < listOfPlayers.Count; i++)
        {
            Instantiate(PlayerEntry, playersList.transform.position + offset, PlayerEntry.transform.rotation, playersList.transform);
            offset += new Vector3(0f, -0.07f, 0f);
        }

    }

    private void LoadSettings()
    {
        listOfPlayers = new List<string>() { "player1", "player2" };
        currentPlayer = PlayerListSettings.Instance.currentPlayer;
    }
}
