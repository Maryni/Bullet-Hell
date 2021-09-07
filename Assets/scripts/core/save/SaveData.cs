using UnityEngine;
using Global.Managers.Datas;

namespace Global.Save
{
    public static class SaveData
    {
        public static string key = "SaveData";

        public static void Save(DynamicData saveObject)
        {
            string json = JsonUtility.ToJson(saveObject);
            PlayerPrefs.SetString(key, json);
        }

        public static DynamicData Load()
        {
            DynamicData dynamicData = new DynamicData();
            string json = PlayerPrefs.GetString(key);

            return JsonUtility.FromJson<DynamicData>(json);
        }

        public static void DefaultSave(DynamicData saveObject)
        {
            if (PlayerPrefs.GetString(key) == null || PlayerPrefs.GetString(key) == "")
            {
                Save(saveObject);
            }
        }

        public static void DeleteAllData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}