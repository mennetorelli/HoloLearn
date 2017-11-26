using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LayTheTableObjectsGenerator {

    // Use this for initialization
    void Start();

    // Update is called once per frame
    void Update();

    Transform GenerateObjects(Transform objectsPrefab, int numberOfPeople);
}
