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


    private void CreateNewXML(object newFile)
    {
        XElement xml =
            new XElement("Players",
                new XElement("Player",
                new XAttribute("PlayerName", "Menne"),
                    new XElement("LayTheTable",
                        new XAttribute("NumberOfLevel", 2),
                        new XAttribute("NumberOfPeople", 2),
                        new XAttribute("TargetsVisibility", 1)),
                    new XElement("GarbageCollection",
                        new XAttribute("numberOfBins", 2),
                        new XAttribute("NumberOfWaste", 5)),
                    new XElement("VirtualAssistant",
                        new XAttribute("SelectedAssistant", 0),
                        new XAttribute("AssistantBehaviour", 2),
                        new XAttribute("AssistantPatience", 5))));
    }

}
