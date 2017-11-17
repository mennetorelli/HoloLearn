using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour {

    public static int LEVEL_SETTINGS;
    public List<GameObject> objectsToBePlaced = new List<GameObject>();
    public List<GameObject> placements = new List<GameObject>();

	// Use this for initialization
	void Start () {
        LEVEL_SETTINGS = 1;

        foreach (GameObject item in objectsToBePlaced)
        {
            Instantiate(item, item.GetComponent<Transform>().position, item.GetComponent<Transform>().rotation);
        }

        foreach (GameObject item in placements)
        {
            Instantiate(item, item.GetComponent<Transform>().position, item.GetComponent<Transform>().rotation);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
