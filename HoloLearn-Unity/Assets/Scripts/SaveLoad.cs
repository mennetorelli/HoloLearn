using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;
using HoloToolkit.Unity;

#if WINDOWS_UWP
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
#endif


// The folder LocalAppData\SOMEAPP\RoamingState on the device portal is mapped to 
// ApplicationData.Current.RoamingFolder.Path in the application at runtime.

public class SaveLoad : Singleton<SaveLoad> {

	// Loading file
	private string ReadString() {
		string s = null;
		#if !UNITY_EDITOR && UNITY_METRO
		try {
		using (Stream stream = OpenFileForRead(ApplicationData.Current.RoamingFolder.Path, "settings.txt")) {
		byte[] data = new byte[stream.Length];
		stream.Read(data, 0, data.Length);
		s = Encoding.ASCII.GetString(data);
		}
		}
		catch (Exception e) {
		Debug.Log(e);
		}
		#endif
		return s;
	}

	private static Stream OpenFileForRead(string folderName, string fileName) {
		Stream stream = null;
		#if !UNITY_EDITOR && UNITY_METRO
		Task task = new Task(
		async () => {
		StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderName);
		StorageFile file = await folder.GetFileAsync(fileName);
		stream = await file.OpenStreamForReadAsync();
		});
		task.Start();
		task.Wait();
		#endif
		return stream;
	}


	// Saving data
	private void WriteString(string s) {
#if !UNITY_EDITOR && UNITY_METRO
		using (Stream stream = OpenFileForWrite(ApplicationData.Current.RoamingFolder.Path, "settings.txt")) {
		byte[] data = Encoding.ASCII.GetBytes(s);
		stream.Write(data, 0, data.Length);
		stream.Flush();
		}
#endif
    }

    private static Stream OpenFileForWrite(string folderName, string fileName) {
		Stream stream = null;
		#if !UNITY_EDITOR && UNITY_METRO
		Task task = new Task(
		async () => {
		StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderName);
		StorageFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

//		// In the above sample code, the file that is queried must exist. In the case when you want to list existing files in a given folder, it is as simple as:
//		foreach (StorageFile f in allFiles) {
//		Debug.Log(TAG + ": found file " + f.Name + ", " + f.DateCreated);
//		}

		stream = await file.OpenStreamForWriteAsync();
		});
		task.Start();
		task.Wait();
		#endif
		return stream;
	}


    public List<int> ReadSettings()
    {
        List<int> settings = new List<int>();
        string settingsStr = ReadString();
        string[] tokens = settingsStr.Split(' ');
        foreach (string token in tokens)
        {
            settings.Add(Convert.ToInt32(token));
        }
        return settings;
    }

    public void WriteSettings(List<int> settings)
    {
        string settingsStr = "";
        foreach (int value in settings)
        {
            settingsStr = settingsStr + value.ToString() + " ";
        }
        WriteString(settingsStr);
    }

}
