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


public class SettingsFileManager : Singleton<SettingsFileManager> {

	public XmlDocument LoadFile()
    {
        XmlDocument xdoc = null;

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
                        CreateNewXML(newFile);
                    }
                    
                    xdoc = await XmlDocument.LoadFromFileAsync(xmlFile);
                });
            task.Start();
            task.Wait();
		}
		catch (Exception e)
        {
		    Debug.Log(e);
		}
#endif
        return xdoc;
	}


	public void UpdateFile()
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
                        CreateNewXML(newFile);
                    }
                    
                    XmlDocument xdoc = await XmlDocument.LoadFromFileAsync(xmlFile);
                    xmlText = xdoc.GetXml();
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


    public XElement CreateNewXML()
    {
        /*XmlDocument xdoc = SettingsFileManager.Instance.LoadFile();
            XElement root = null;
            if (xdoc != null)
            {
                root = XElement.Load(new XmlNodeReader(xdoc));
            }*/


        XElement root =
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

        return root;
    }

}
