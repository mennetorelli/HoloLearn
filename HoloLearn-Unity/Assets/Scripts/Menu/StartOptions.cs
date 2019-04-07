using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HoloLearn
{
    public class StartOptions : MonoBehaviour
    {

        public void Start()
        {
            SettingsFileManager.Instance.CreateFileIfNotExists();
            SettingsFileManager.Instance.LoadCurrentPlayerSettings(SettingsFileManager.Instance.LoadCurrentPlayerSelection());
        }

        public void ChangeScene(int scene)
        {
            if (scene == 0)
            {
                for (int i = 1; i < SceneManager.sceneCount; i++)
                {
                    SceneManager.UnloadSceneAsync(i);
                }
                if (VirtualAssistantManager.Instance != null)
                {
                    Destroy(VirtualAssistantManager.Instance.gameObject);
                }
                Destroy(TaskManager.Instance.gameObject);
                Destroy(GameObject.Find("Settings"));
                Destroy(GameObject.Find("SpatialMapping"));
                Destroy(GameObject.Find("SpatialProcessing"));
            }
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        public void RestartScene()
        {
            TaskManager.Instance.DestroyObjects();
            GameObject.Find("TaskMenu").GetComponent<TaskInteractionHandler>().ScanningComplete();
        }
    }
}

