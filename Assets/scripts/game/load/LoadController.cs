using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Shooting.BulletSpace;
using Global.Interfaces.Weapon;
using Global.Shooting;

namespace Global.Controllers
{
    public class LoadController : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private BulletManager bulletPool;
        [SerializeField] private ShootController shootController;
        [SerializeField] private DataManager dataManager;
        [SerializeField] private WeaponSettings[] settingsWeapon;
        [SerializeField] private BaseWeapon baseWeapon;
        [SerializeField] private SpawnController spawnController;
#pragma warning restore

        #endregion private variables

        #region public void

        public void LoadSettingsWeapon() => baseWeapon.LoadSettings(settingsWeapon[shootController.WeaponType]);

        public void LoadSettingsWeapon(int typeWeapon) => baseWeapon.LoadSettings(settingsWeapon[typeWeapon]);

        public void RestoreTimeScale() => Time.timeScale = 1f;

        #endregion public void

        #region private void

        private void PauseOnStart() => Time.timeScale = 0f;

        private void LoadSettingsFromData()
        {
            var tempTimer1 = float.Parse(dataManager.DynamicData.ArrayData[0]);
            var tempTimer2 = float.Parse(dataManager.DynamicData.ArrayData[1]);
            spawnController.LoadSettings(tempTimer1, tempTimer2);
            var temp = float.Parse(dataManager.DynamicData.ArrayData[2]);
            bulletPool.SetTimerToBlowUpRocket(temp);
            int temp2 = int.Parse(dataManager.DynamicData.ArrayData[3]);
            shootController.ChangeWeaponType(temp2);
        }

        #region Unity function

        private void Start()
        {
            LoadSettingsFromData();
            LoadSettingsWeapon();
        }

        private void Awake()
        {
            PauseOnStart();
        }

        #endregion Unity function

        #endregion private void
    }
}