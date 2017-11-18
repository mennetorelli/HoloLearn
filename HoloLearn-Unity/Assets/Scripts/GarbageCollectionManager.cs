using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GarbageCollectionManager : MonoBehaviour
{

    public static int GARBAGE_COLLECTION_LEVEL;
    public GameObject garbageCollectionObjects;

    // Use this for initialization
    void Start()
    {
        GARBAGE_COLLECTION_LEVEL = 2;

        //Per scegliere a seconda del livello
        Transform selectedLevel = garbageCollectionObjects.transform.GetChild(GARBAGE_COLLECTION_LEVEL - 1);

        //Da creare un metodo per posizionare gli oggetti
        Transform bins = selectedLevel.transform.GetChild(0);
        Instantiate(bins.gameObject, bins.position, bins.rotation);

        //Da creare un metodo per posizionare gli oggetti
        Transform waste = selectedLevel.transform.GetChild(1);
        Instantiate(waste.gameObject, waste.position, waste.rotation);
    }
}
