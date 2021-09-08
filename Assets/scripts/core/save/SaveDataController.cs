using Global.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    public void DeleteData()
    {
        SaveData.DeleteAllData();
    }
}