using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuScene : MonoBehaviour {

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
