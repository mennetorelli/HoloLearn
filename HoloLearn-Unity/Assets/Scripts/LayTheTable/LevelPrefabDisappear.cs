using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelPrefabDisappear : Singleton<LevelPrefabDisappear> {

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void MakeLevelPrefabDisappear(GameObject draggedObject);
    
}
