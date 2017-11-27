using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageCollectionManager : MonoBehaviour
{

    public GameObject BinsPrefabs;
    public GameObject WastePrefabs;

    private int numberOfBins;
    private int numberOfObjects;

    private List<string> activeBins = new List<string>();

    // Use this for initialization
    void Start()
    {
        numberOfBins = 2;
        numberOfObjects = 10;

        GenerateObjectsInWorld(null, null);
    }

    internal void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il pavimento
        //Transform floor = floors.ElementAt(0).transform;

        Transform bins = new GameObject("Bins").transform;

        System.Random rnd = new System.Random();


        for (int i=1; i<=numberOfBins;)
        {
            Transform bin = BinsPrefabs.transform.GetChild(rnd.Next(0, BinsPrefabs.transform.childCount));
            String currentBinTag = bin.gameObject.tag;
            if (!activeBins.Contains(currentBinTag))
            {
                Instantiate(bin, new Vector3((float)Math.Pow(-1, i) * 0.4f * (i / 2), 0f, 4f), bin.rotation, bins);
                activeBins.Add(bin.gameObject.tag);
                i++;
            }
        }


        for (int i=0; i<numberOfObjects;)
        {
            Transform waste = WastePrefabs.transform.GetChild(rnd.Next(0, WastePrefabs.transform.childCount));
            String currentWasteTag = waste.gameObject.tag;
            if (activeBins.Contains(currentWasteTag))
            {
                Instantiate(waste, new Vector3(0f, 0f, 1f), waste.rotation);
                i++;
            }
        }
    
    }
}
