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
        numberOfBins = 3;
        numberOfObjects = 4;

        GenerateObjectsInWorld(null, null);
    }

    internal void GenerateObjectsInWorld(List<GameObject> floors, List<GameObject> tables)
    {
        //Seleziono il pavimento
        //Transform floor = floors.ElementAt(0).transform;

        Transform bins = new GameObject("Bins").transform;

        System.Random rnd = new System.Random();


        //DA TOGLIERE IL BIDONE SE è GIà STATO MESSO
        for (int i=1; i<=numberOfBins;)
        {
            Transform bin = BinsPrefabs.transform.GetChild(rnd.Next(0, BinsPrefabs.transform.childCount));
            String currentBinTag = bin.gameObject.tag;
            Debug.Log(currentBinTag);
            if (!activeBins.Contains(currentBinTag))
            {
                Instantiate(bin, new Vector3((float)Math.Pow(-1, i) * 0.4f * (i / 2), 0f, 4f), bin.rotation, bins);
                activeBins.Add(bin.gameObject.tag);
                i++;
            }
        }


        for (int i=0; i<numberOfObjects; i++)
        {
            Transform waste = WastePrefabs.transform.GetChild(rnd.Next(0, WastePrefabs.transform.childCount));
            Instantiate(waste, new Vector3(0f, 0f, 1f), waste.rotation);
        }
    
    }
}
