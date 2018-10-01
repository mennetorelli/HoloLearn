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

    public XElement LoadFile()
    {
        XElement root = null;

#if !UNITY_EDITOR && UNITY_METRO
        
        Task<Task> task = new Task<Task>(async () =>
            {
                try
                {
		            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    string xmlText = await FileIO.ReadTextAsync(xmlFile);
                    root = XElement.Parse(xmlText);
                    Debug.Log(root);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait(); 
        task.Result.Wait();

#endif
        return root;
    }


    public void UpdateFile(XElement root)
    {

#if !UNITY_EDITOR && UNITY_METRO
		  
       Task<Task> task = new Task<Task>(async () =>
            {   
                try
                {
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    string xmlText = root.ToString();
                    await FileIO.WriteTextAsync(xmlFile, xmlText);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait(); 
        task.Result.Wait();
        
#endif
    }


    public void CreateFileIfNotExists()
    {

#if !UNITY_EDITOR && UNITY_METRO
		  
        Task<Task> task = new Task<Task>(async () =>
            {   
                try
                { 
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.CreateFileAsync("settings.xml", CreationCollisionOption.OpenIfExists);
                    string xmlText = await FileIO.ReadTextAsync(xmlFile);
                    if (xmlText == "")
                    {
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
        task.Result.Wait();

#endif
    }
}
