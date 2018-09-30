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
            SettingsFileManager.Instance.LoadFile();
            XElement root = SettingsFileManager.Instance.GetXML();

            IEnumerable <XElement> players =
                from item in root.Elements("Player")
                select item;

            foreach (XElement item in players)
            {
                PlayerListSettings.Instance.listOfPlayers.Add((string)item.Attribute("PlayerName"));
            }
            PlayerListSettings.Instance.currentPlayer = (int)root.Attribute("CurrentPlayer");
            

            IEnumerable<XElement> layTheTableSettings =
                from item in root.Elements("Player")
                where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
                select item.Element("LayTheTableSettings");

            LayTheTableSettings.Instance.numberOfLevel = (int)layTheTableSettings.ElementAt(0).Attribute("NumberOfLevel");
            LayTheTableSettings.Instance.numberOfPeople = (int)layTheTableSettings.ElementAt(0).Attribute("NumberOfPeople");
            LayTheTableSettings.Instance.targetsVisibility = (int)layTheTableSettings.ElementAt(0).Attribute("TargetsVisibility");


            IEnumerable<XElement> garbageCollectionSettings =
                from item in root.Elements("Player")
                where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
                select item.Element("GarbageCollectionSettings");

            GarbageCollectionSettings.Instance.numberOfBins = (int)garbageCollectionSettings.ElementAt(0).Attribute("NumberOfBins");
            GarbageCollectionSettings.Instance.numberOfWaste = (int)garbageCollectionSettings.ElementAt(0).Attribute("NumberOfWaste");


            IEnumerable<XElement> virtualAssistantChoice =
                from item in root.Elements("Player")
                where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
                select item.Element("VirtualAssistantChoice");

            VirtualAssistantChoice.Instance.assistantPresence = (int)virtualAssistantChoice.ElementAt(0).Attribute("AssistantPresence");
            VirtualAssistantChoice.Instance.selectedAssistant = (int)virtualAssistantChoice.ElementAt(0).Attribute("SelectedAssistant");


            IEnumerable<XElement> virtualAssistantSettings =
                from item in root.Elements("Player")
                where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
                select item.Element("VirtualAssistantSettings");

            VirtualAssistantSettings.Instance.assistantBehaviour = (int)virtualAssistantSettings.ElementAt(0).Attribute("AssistantBehaviour");
            VirtualAssistantSettings.Instance.assistantPatience = (int)virtualAssistantSettings.ElementAt(0).Attribute("AssistantPatience");
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

