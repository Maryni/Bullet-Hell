using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadController : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private ShootController shootController;
    [SerializeField] private DataManager dataManager;

    private void LoadSettingsFromData()
    {
        var temp = float.Parse(dataManager.DynamicData.ArrayData[2]);
        bulletPool.SetTimerToBlowUpRocket(temp);
        int temp2 = int.Parse(dataManager.DynamicData.ArrayData[3]);
        shootController.ChangeWeaponType(temp2);
    }

    private void Awake()
    {
        LoadSettingsFromData();
    }
}