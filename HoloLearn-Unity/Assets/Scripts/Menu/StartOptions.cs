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
                //distruggere tutti i singleton
            }
            SceneManager.LoadScene(scene);
        }
    }
}

