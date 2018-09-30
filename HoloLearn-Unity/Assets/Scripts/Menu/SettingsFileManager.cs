using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;
using HoloToolkit.Unity;
using System.Xml.Linq;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif


public class SettingsFileManager : Singleton<SettingsFileManager>
{
    private XElement root;
    public XElement GetXML()
    {
        return root;
    }
    public void SetXML(XElement root)
    {
        this.root = root;
    }


    public void LoadFile()
    {

#if !UNITY_EDITOR && UNITY_METRO
        
        Task task = new Task(
            async () =>
            {
                try
                {
		            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    string xmlText = await FileIO.ReadTextAsync(xmlFile);
                    root = XElement.Parse(xmlText);
                    Debug.Log(root);

                    IEnumerable <XElement> players =
                        from item in root.Elements("Player")
                        select item;

                    foreach (XElement item in players)
                    {
                        PlayerListSettings.Instance.listOfPlayers.Add((string)item.Attribute("PlayerName"));
                    }
                    PlayerListSettings.Instance.currentPlayer = (int)root.Attribute("CurrentPlayer");
                    Debug.Log(PlayerListSettings.Instance.currentPlayer);

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
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait();
#endif
    }


    public void UpdateFile()
    {

#if !UNITY_EDITOR && UNITY_METRO
		  
        Task task = new Task(
            async () =>
            {   
                try
                { 
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    string xmlText = root.ToString();
                    //Debug.Log(xmlText);
                    await FileIO.WriteTextAsync(xmlFile, xmlText);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait();
        
#endif
    }


    public void CreateFileIfNotExists()
    {

#if !UNITY_EDITOR && UNITY_METRO
		  
        Task task = new Task(
            async () =>
            {   
                try
                { 
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.CreateFileAsync("settings.xml", CreationCollisionOption.OpenIfExists);
                    string xmlText = await FileIO.ReadTextAsync(xmlFile);
                    if (xmlText == "")
                    {
                        root =
                            new XElement("Players",
                            new XAttribute("CurrentPlayer", 1),
                                new XElement("Player",
                                new XAttribute("PlayerName", "Player 1"),
                                    new XElement("LayTheTableSettings",
                                        new XAttribute("NumberOfLevel", 3),
                                        new XAttribute("NumberOfPeople", 1),
                                        new XAttribute("TargetsVisibility", 1)),
                                    new XElement("GarbageCollectionSettings",
                                        new XAttribute("NumberOfBins", 2),
                                        new XAttribute("NumberOfWaste", 5)),
                                    new XElement("VirtualAssistantChoice",
                                        new XAttribute("AssistantPresence", 0),
                                        new XAttribute("SelectedAssistant", 1)),
                                    new XElement("VirtualAssistantSettings",
                                        new XAttribute("AssistantBehaviour", 2),
                                        new XAttribute("AssistantPatience", 5))),
                            new XElement("Player",
                                new XAttribute("PlayerName", "Player 2"),
                                    new XElement("LayTheTableSettings",
                                        new XAttribute("NumberOfLevel", 1),
                                        new XAttribute("NumberOfPeople", 3),
                                        new XAttribute("TargetsVisibility", 0)),
                                    new XElement("GarbageCollectionSettings",
                                        new XAttribute("NumberOfBins", 3),
                                        new XAttribute("NumberOfWaste", 8)),
                                    new XElement("VirtualAssistantChoice",
                                        new XAttribute("AssistantPresence", 1),
                                        new XAttribute("SelectedAssistant", 0)),
                                    new XElement("VirtualAssistantSettings",
                                        new XAttribute("AssistantBehaviour", 1),
                                        new XAttribute("AssistantPatience", 5))));
                        xmlText = root.ToString();   
                        await FileIO.WriteTextAsync(xmlFile, xmlText);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait();
#endif
    }
}
