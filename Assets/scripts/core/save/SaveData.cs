using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Global.Managers.Datas;

namespace Global.Save
{
    public static class SaveData
    {
        public static string directory = "/SaveData/";
        public static string fileName = "data.json";

        public static void Save(DynamicData saveObject)
        {
            string dir = Application.persistentDataPath + directory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string json = JsonUtility.ToJson(saveObject);
            File.WriteAllText(dir + fileName, json);
        }

        public static DynamicData Load()
        {
            string fullPath = Application.persistentDataPath + directory + fileName;
            DynamicData dynamicData = new DynamicData();
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                dynamicData = JsonUtility.FromJson<DynamicData>(json);
            }
            else
            {
                Debug.Log("File are not exist");
            }
            return dynamicData;
        }
    }
}