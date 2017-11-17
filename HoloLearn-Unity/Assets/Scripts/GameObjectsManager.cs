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

        //Per scegliere il livello
        Transform selectedLevel = layTheTableObjects.transform.GetChild(LEVEL_SETTINGS - 1);

        //Da mandare ad un metodo apposito
        Transform objectsToBePlaced = selectedLevel.transform.GetChild(0);
        Instantiate(objectsToBePlaced.gameObject, objectsToBePlaced.position, objectsToBePlaced.rotation);

        //Da mandare ad un metodo apposito
        Transform placements = selectedLevel.transform.GetChild(1);
        Instantiate(placements.gameObject, placements.position, placements.rotation);
     }
}
