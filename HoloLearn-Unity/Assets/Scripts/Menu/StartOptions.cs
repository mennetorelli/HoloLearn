using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace HoloLearn
{
    public class StartOptions : MonoBehaviour
    {
        public void ChangeScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}

