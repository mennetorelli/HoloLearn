using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity;

namespace HoloLearn
{
    public class StartOptions : MonoBehaviour
    {
        public void ChangeScene(int scene)
        {
            if (scene == 0)
            {
                foreach (GameObject item in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    Debug.Log(item);
                    // eliminare tutti i singleton
                }
            }
            SceneManager.LoadScene(scene);
        }
    }
}

