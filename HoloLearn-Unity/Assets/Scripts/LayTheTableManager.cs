using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayTheTableManager : MonoBehaviour {

    public static int LAY_THE_TABLE_LEVEL;
    public GameObject layTheTableObjects;

	// Use this for initialization
	void Start () {
        LAY_THE_TABLE_LEVEL = 1;

        //Per scegliere a seconda del livello
        Transform selectedLevel = layTheTableObjects.transform.GetChild(LAY_THE_TABLE_LEVEL - 1);

        //Da creare un metodo per posizionare gli oggetti
        Transform objectsToBePlaced = selectedLevel.transform.GetChild(0);
        Instantiate(objectsToBePlaced.gameObject, objectsToBePlaced.position, objectsToBePlaced.rotation);

        //Da creare un metodo per posizionare gli oggetti
        Transform placements = selectedLevel.transform.GetChild(1);
        Instantiate(placements.gameObject, placements.position, placements.rotation);
     }
}
