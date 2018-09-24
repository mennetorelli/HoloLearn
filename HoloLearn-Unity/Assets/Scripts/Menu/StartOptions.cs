using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;

namespace HoloLearn
{
    public class StartOptions : MonoBehaviour
    {

        public void Start()
        {

        }

        public void ChangeScene(int scene)
        {
            scene++;
            if (scene == 1)
            {
                Destroy(VirtualAssistantManager.Instance.gameObject);
                Destroy(TaskManager.Instance.gameObject);
                Destroy(GameObject.Find("Settings"));
                Destroy(GameObject.Find("SpatialMapping"));
                Destroy(GameObject.Find("SpatialProcessing"));
            }
            SceneManager.LoadScene(scene);
        }
    }
}

