using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabDisappearLvl1 : LevelPrefabDisappear {
   

    // Use this for initialization
    public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public override void MakeLevelPrefabDisappear(GameObject draggedObject)
    {
        Rigidbody[] disappearingObjects = GameObject.Find("TableMatePlacement").GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody rb in disappearingObjects)
        {
            if(draggedObject.tag != rb.tag)
            {
                rb.gameObject.SetActive(false);
            }
        }
    }
}
