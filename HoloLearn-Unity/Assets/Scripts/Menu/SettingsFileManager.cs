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
                    Debug.Log(xmlFile.DisplayName);
                    if (xmlFile != null)
                    {
                        /*XmlDocument xmlDoc = await XmlDocument.LoadFromFileAsync(xmlFile);
                        Debug.Log(xmlDoc);
                        string xmlText = xmlDoc.GetXml();
                        Debug.Log(xmlText);*/
                        string xmlText = await FileIO.ReadTextAsync(xmlFile);
                        Debug.Log(xmlText);
                        root = XElement.Parse(xmlText);
                        Debug.Log(root);
                    }
                    else
                    {
                        Debug.Log("file non trovato");
                        StorageFile newFile = await storageFolder.CreateFileAsync("settings.xml");
                        Debug.Log("file creato");
                        CreateNewXML();
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("reading problem");
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait();
#endif
    }


    public void UpdateFile(XElement root)
    {

#if !UNITY_EDITOR && UNITY_METRO
		  
        Task task = new Task(
            async () =>
            {   
                try
                { 
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    Debug.Log(xmlFile.DisplayName);
                    string xmlText = root.ToString();
                    Debug.Log(xmlText);
                    await FileIO.WriteTextAsync(xmlFile, xmlText);
                    Debug.Log("ok");
                }
                catch (Exception e)
                {
                    Debug.Log("writing problem");
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait();
        
#endif
    }


    public void CreateNewXML()
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
    }

}
