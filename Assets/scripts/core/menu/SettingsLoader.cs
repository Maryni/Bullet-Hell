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
        [Header("Start Types"), SerializeField] private Dropdown startWeapon;
        [SerializeField] private Dropdown cameraType;

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
        [SerializeField] private InputField shotgunAngleBullet;
        [SerializeField] private InputField shotgunCountBulletsPerShoot;

        [Header("RocketLaucher"), SerializeField]
        private InputField rocketLaucherCountBullets;

        [SerializeField] private InputField rocketLaucherCooldownTime;
        [SerializeField] private InputField rocketLaucherShootingRate;
        [SerializeField] private InputField rocketLaucherBulletDamage;
        [SerializeField] private InputField rocketLaucherBulletMinSpeed;
        [SerializeField] private InputField rocketLaucherBulletMaxSpeed;
        [SerializeField] private InputField rocketLaucherBulletTimeAcceleration;
        [SerializeField] private InputField rocketTimerBlowUp;
        [SerializeField] private InputField rocketRadiusBlowUp;

        [Header("Spawn Weapon"), SerializeField]
        private InputField timerToSpawnItemWeapon;

        [SerializeField] private InputField timerToDisableItemWeapon;

        [Header("Spawn PowerUp"), SerializeField]
        private InputField timerToSpawnItemPowerUp;

        [SerializeField] private InputField timerToDisableItemPowerUp;
        [SerializeField] private InputField durationTime;
        [SerializeField] private InputField killAreaRadius;

        [Header("Player"), SerializeField]
        private InputField playerSpeed;

        [SerializeField] private InputField playerHp;

        [Header("Camera"), SerializeField]
        private InputField cameraDistance;

        [Header("Pause"), SerializeField]
        private InputField pauseTime;

        [Header("Slow enemy"), SerializeField] private InputField slowEnemySpeed;
        [Header("Middle enemy"), SerializeField] private InputField middleEnemySpeed;
        [Header("Fast enemy"), SerializeField] private InputField fastEnemySpeed;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            var data = Services.GetManager<DataManager>().DynamicData;
            LoadSettings(automaticCountBullets, data.GetWeaponDataByType(WeaponType.AutomaticGun).bulletCount.ToString());
            LoadSettings(automaticCooldownTime, data.GetWeaponDataByType(WeaponType.AutomaticGun).cooldownTime.ToString());
            LoadSettings(automaticShootingRate, data.GetWeaponDataByType(WeaponType.AutomaticGun).shootingRate.ToString());
            LoadSettings(automaticalBulletDamage, data.GetBulletDataByType(BulletType.AutomaticBullet).damage.ToString());
            LoadSettings(automaticalBulletSpeed, data.GetBulletDataByType(BulletType.AutomaticBullet).speed.ToString());

            LoadSettings(shotgunCountBullets, data.GetWeaponDataByType(WeaponType.Shotgun).bulletCount.ToString());
            LoadSettings(shotgunCooldownTime, data.GetWeaponDataByType(WeaponType.Shotgun).cooldownTime.ToString());
            LoadSettings(shotgunShootingRate, data.GetWeaponDataByType(WeaponType.Shotgun).shootingRate.ToString());
            LoadSettings(shotgunAngleBullet, data.ShotgunData.angleBullets.ToString());
            LoadSettings(shotgunCountBulletsPerShoot, data.ShotgunData.countBulletsInOnceShoot.ToString());
            LoadSettings(shotgunBulletDamage, data.GetBulletDataByType(BulletType.ShotgunBullet).damage.ToString());
            LoadSettings(shotgunBulletSpeed, data.GetBulletDataByType(BulletType.ShotgunBullet).speed.ToString());

            LoadSettings(rocketLaucherCountBullets, data.GetWeaponDataByType(WeaponType.RocketLaucher).bulletCount.ToString());
            LoadSettings(rocketLaucherCooldownTime, data.GetWeaponDataByType(WeaponType.RocketLaucher).cooldownTime.ToString());
            LoadSettings(rocketLaucherShootingRate, data.GetWeaponDataByType(WeaponType.RocketLaucher).shootingRate.ToString());
            LoadSettings(rocketLaucherBulletDamage, data.GetBulletDataByType(BulletType.RocketLaucherBullet).damage.ToString());
            LoadSettings(rocketLaucherBulletMinSpeed, data.RocketData.maxSpeed.ToString());
            LoadSettings(rocketLaucherBulletMaxSpeed, data.RocketData.maxSpeed.ToString());
            LoadSettings(rocketLaucherBulletTimeAcceleration, data.RocketData.timeAcceleration.ToString());

            LoadSettings(rocketTimerBlowUp, data.RocketData.timeToBlowUp.ToString());
            LoadSettings(rocketRadiusBlowUp, data.RocketData.radiusBlowUp.ToString());

            LoadSettings(timerToSpawnItemWeapon, data.WeaponSpawnItemData.spawnTime.ToString());
            LoadSettings(timerToDisableItemWeapon, data.WeaponSpawnItemData.destroyTime.ToString());

            LoadSettings(timerToSpawnItemPowerUp, data.PowerUpSpawnItemData.spawnTime.ToString());
            LoadSettings(timerToDisableItemPowerUp, data.PowerUpSpawnItemData.destroyTime.ToString());
            LoadSettings(durationTime, data.PowerUpSpawnItemData.duration.ToString());
            LoadSettings(killAreaRadius, data.PowerUpSpawnItemData.killAreaRadius.ToString());

            LoadSettings(playerHp, data.PlayerData.hp.ToString());
            LoadSettings(playerSpeed, data.PlayerData.speed.ToString());
            LoadSettings(startWeapon, data.StartPlayerWeapon.ToString());
            LoadSettings(cameraType, data.StartCameraType.ToString());
            LoadSettings(cameraDistance, data.CameraData.cameraDistance.ToString());
            LoadSettings(pauseTime, data.PauseData.pauseTime.ToString());

            LoadSettings(slowEnemySpeed, data.GetEnemyStatsByType(EnemyType.MeleeGrounded_LowSpeed).speed.ToString());
            LoadSettings(middleEnemySpeed, data.GetEnemyStatsByType(EnemyType.MeleeGrounded_MiddleSpeed).speed.ToString());
            LoadSettings(fastEnemySpeed, data.GetEnemyStatsByType(EnemyType.MeleeGrounded_FastSpeed).speed.ToString());
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