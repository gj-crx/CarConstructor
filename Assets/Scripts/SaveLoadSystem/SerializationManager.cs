using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace SaveLoadSystem
{
    public static class SerializationManager
    {
        public static void SaveObject(string saveName, string saveDirectory, object dataToSave)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string savePath = saveDirectory + saveName;

            try
            {
                FileStream saveFile = File.Create(savePath);
                formatter.Serialize(saveFile, dataToSave);
                saveFile.Close();
                Debug.Log(dataToSave + " saved as " + savePath);
            }
            catch
            {
                LogSaver.Singleton.AddLog("failed to save " + savePath);
            }
        }

        public static object LoadObject(string saveToLoadPath)
        {
            if (File.Exists(saveToLoadPath) == false)
            {
                Debug.LogError("Save not found! " + saveToLoadPath);
                LogSaver.Singleton.AddLog("save not found " + saveToLoadPath);
                return null;
            }

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = File.Open(saveToLoadPath, FileMode.Open);
            try
            {
                System.Object loaded = formatter.Deserialize(file);
                file.Close();
                return loaded;
            }
            catch
            {
                Debug.LogError("Failed to load " + saveToLoadPath);
                LogSaver.Singleton.AddLog("failed to load " + saveToLoadPath);
                file.Close();
                return null;
            }
        }
        public static void SaveObjectAsAsset(GameObject asset, string SaveName, string directory = "")
        {
                Debug.LogError("RELEASE BUILD DETECTED");

            if (directory == "")
            {
            //    PrefabUtility.SaveAsPrefabAsset(asset, "Assets/Resources/" + SaveName + ".prefab");

            }
            else
            {
                if (Directory.Exists("Assets/Resources/" + directory + "/") == false) Directory.CreateDirectory("Assets/Resources/" + directory + "/");
                //    PrefabUtility.SaveAsPrefabAsset(asset, "Assets/Resources/" + directory + "/" + SaveName + ".prefab");
            }

        }

    }
}
