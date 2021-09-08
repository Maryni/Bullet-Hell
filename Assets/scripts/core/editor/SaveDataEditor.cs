using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Global.Managers.Datas
{
    [CustomEditor(typeof(SaveDataController))]
    public class SaveDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SaveDataController dataManagerScript = (SaveDataController)target;
            if (GUILayout.Button("Delete data"))
            {
                dataManagerScript.DeleteData();
            }
        }
    }
}