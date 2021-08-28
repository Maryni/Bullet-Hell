using Global.Bullet;
using Global.Managers.Datas;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace Global.UI
{
    public class SettingsLoader : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Dropdown startWeapon;
        [SerializeField] private Dropdown cameraType;

        [Header("WeaponStats"), SerializeField]
        private WeaponStats automaticStatsFile;

        [SerializeField] private WeaponStats shotgunStatsFile;
        [SerializeField] private WeaponStats rocketLaucherStatsFile;

        [Header("BulletsGameObject"), SerializeField]
        private GameObject automaticalBulletGameObject;

        [SerializeField] private GameObject shotgunBulletGameObject;
        [SerializeField] private GameObject rocketLaucherBulletGameObject;

        [Header("Automatic"), SerializeField]
        private InputField automaticCountBullets;

        [SerializeField] private InputField automaticCooldownTime;
        [SerializeField] private InputField automaticShootingRate;
        [SerializeField] private InputField automaticalBulletDamage;
        [SerializeField] private InputField automaticalBulletSpeed;

        [Header("Shotgun"), SerializeField]
        private InputField shotgunCountBullets;

        [SerializeField] private InputField shotgunCooldownTime;
        [SerializeField] private InputField shotgunShootingRate;
        [SerializeField] private InputField shotgunBulletDamage;
        [SerializeField] private InputField shotgunBulletSpeed;

        [Header("RocketLaucher"), SerializeField]
        private InputField rocketLaucherCountBullets;

        [SerializeField] private InputField rocketLaucherCooldownTime;
        [SerializeField] private InputField rocketLaucherShootingRate;
        [SerializeField] private InputField rocketLaucherBulletDamage;
        [SerializeField] private InputField rocketLaucherBulletSpeed;
        [SerializeField] private InputField rocketTimerBlowUp;
        [SerializeField] private InputField rocketRadiusBlowUp;

        [Header("SpawnItem"), SerializeField]
        private InputField timerToSpawnItem;

        [SerializeField] private InputField timerToDisableItems;

        [Header("Player"), SerializeField]
        private InputField playerSpeed;

        [Header("Camera"), SerializeField]
        private InputField cameraDistance;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            var data = Services.GetManager<DataManager>().DynamicData;
            LoadSettings(automaticCountBullets, automaticStatsFile.bulletCount.ToString());
            LoadSettings(automaticCooldownTime, automaticStatsFile.cooldownTime.ToString());
            LoadSettings(automaticShootingRate, automaticStatsFile.shootingRate.ToString());
            LoadSettings(automaticalBulletDamage, automaticalBulletGameObject.GetComponent<AutomaticalBullet>().BulletStats.damage.ToString());
            LoadSettings(automaticalBulletSpeed, automaticalBulletGameObject.GetComponent<AutomaticalBullet>().BulletStats.speed.ToString());

            LoadSettings(shotgunCountBullets, shotgunStatsFile.bulletCount.ToString());
            LoadSettings(shotgunCooldownTime, shotgunStatsFile.cooldownTime.ToString());
            LoadSettings(shotgunShootingRate, shotgunStatsFile.shootingRate.ToString());
            LoadSettings(shotgunBulletDamage, shotgunBulletGameObject.GetComponent<ShotgunBullet>().BulletStats.damage.ToString());
            LoadSettings(shotgunBulletSpeed, shotgunBulletGameObject.GetComponent<ShotgunBullet>().BulletStats.speed.ToString());

            LoadSettings(rocketLaucherCountBullets, rocketLaucherStatsFile.bulletCount.ToString());
            LoadSettings(rocketLaucherCooldownTime, rocketLaucherStatsFile.cooldownTime.ToString());
            LoadSettings(rocketLaucherShootingRate, rocketLaucherStatsFile.shootingRate.ToString());
            LoadSettings(rocketLaucherBulletDamage, rocketLaucherBulletGameObject.GetComponent<RocketLaucherBullet>().BulletStats.damage.ToString());
            LoadSettings(rocketLaucherBulletSpeed, rocketLaucherBulletGameObject.GetComponent<RocketLaucherBullet>().BulletStats.speed.ToString());
            LoadSettings(rocketTimerBlowUp, data.RocketData.timeToBlowUp.ToString());
            LoadSettings(rocketRadiusBlowUp, data.RocketData.radiusBlowUp.ToString());

            LoadSettings(timerToSpawnItem, data.SpawnItemData.spawnTime.ToString());
            LoadSettings(timerToDisableItems, data.SpawnItemData.destroyTime.ToString());
            LoadSettings(playerSpeed, data.PlayerData.speed.ToString());
            LoadSettings(startWeapon, data.StartPlayerWeapon.ToString());
            LoadSettings(cameraType, data.StartCameraType.ToString());
            LoadSettings(cameraDistance, data.CameraData.cameraDistance.ToString());
        }

        #endregion Unity functions

        #region private void

        private void LoadSettings(InputField input, string value)
        {
            input.text = value;
        }

        private void LoadSettings(Dropdown input, string value)
        {
            for (int i = 0; i < input.options.Count; i++)
            {
                if (input.options[i].text == value)
                {
                    input.value = i;
                }
            }
        }

        #endregion private void
    }
}