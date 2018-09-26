using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;
using HoloToolkit.Unity;
using System.Xml.Linq;
using System.Xml;


#if WINDOWS_UWP
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
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
		try {
		    Task task = new Task(
                async () =>
                {
                    //Get local folder
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

                    //Get file
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");

                    //Create file if doesn't exist
                    if (xmlFile == null)
                    {
                        StorageFile newFile = await storageFolder.CreateFileAsync("settings.xml");
                    }
                    
                    XmlDocument xmlDoc = await XmlDocument.LoadFromFileAsync(xmlFile);
                    XDocument xDoc = XDocument.Load(new XmlNodeReader(xmlDoc));
                    root=xDoc.Root;

                });
            task.Start();
            task.Wait();
		}
		catch (Exception e)
        {
		    Debug.Log(e);
		}
#endif
    }


    public void UpdateFile(XElement root)
    {

#if !UNITY_EDITOR && UNITY_METRO
        try {
		    Task task = new Task(
                async () =>
                {
                    //Get local folder
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

                    //Get file
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");

                    //Create file if doesn't exist
                    if (xmlFile == null)
                    {
                        StorageFile newFile = await storageFolder.CreateFileAsync("settings.xml");
                    }
                    XmlDocument xmlDoc = await XmlDocument.LoadFromFileAsync(xmlFile);
                    XDocument xDoc = XDocument.Load(new XmlNodeReader(xmlDoc));
                    root = xDoc.Root;

                });
            task.Start();
            task.Wait();
		}
		catch (Exception e)
        {
		    Debug.Log(e);
		}
#endif
    }


    public void CreateNewXML()
    {
        /*XmlDocument xdoc = SettingsFileManager.Instance.LoadFile();
            XElement root = null;
            if (xdoc != null)
            {
                root = XElement.Load(new XmlNodeReader(xdoc));
            }*/


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
