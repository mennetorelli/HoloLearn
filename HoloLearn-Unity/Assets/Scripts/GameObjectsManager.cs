using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour {

    public static int LEVEL_SETTINGS;
    public GameObject layTheTableObjects;

	// Use this for initialization
	void Start () {
        LEVEL_SETTINGS = 2;

        Transform selectedLevel = layTheTableObjects.transform.GetChild(LEVEL_SETTINGS - 1);

        Transform objectsToBePlaced = selectedLevel.transform.GetChild(0);
        foreach (Transform item in objectsToBePlaced)
        {
            Instantiate(item.gameObject, item.GetComponent<Transform>().position, item.GetComponent<Transform>().rotation);
        }

        Transform placements = selectedLevel.transform.GetChild(1);
        foreach (Transform item in placements)
        {
            Instantiate(item.gameObject, item.GetComponent<Transform>().position, item.GetComponent<Transform>().rotation);
        }
    }
}
