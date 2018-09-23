using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerButtonCommand : MonoBehaviour {

    public void DeletePlayerEntry(GameObject caller)
    {
        GameObject.Find("PlayerMenu").GetComponent<PlayerListSettingsManager>().DeletePlayerEntry(caller);
    }
}
