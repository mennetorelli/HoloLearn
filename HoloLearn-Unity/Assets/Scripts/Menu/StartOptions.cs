using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity;
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

        public void BackToMainMenu()
        {
            TaskManager.Instance.DestroyObjects();

            Destroy(TaskManager.Instance.gameObject);
            Destroy(GameObject.Find("SpatialMapping"));
            Destroy(GameObject.Find("SpatialProcessing"));

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));

            GameObject.Find("MenuPrefab").SetActive(true);
        }

        public void ChangeScene(int scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        public void RestartScene()
        {
            TaskManager.Instance.DestroyObjects();
            GameObject.Find("TaskMenu").GetComponent<TaskInteractionHandler>().ScanningComplete();
        }
    }
}

