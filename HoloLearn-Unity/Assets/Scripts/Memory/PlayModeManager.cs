using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayModeManager : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract List<Transform> GenerateObjects(GameObject ObjectsPrefabs, int numberOfBoxes);

    public abstract void StartGame(int waitingTime);

}
