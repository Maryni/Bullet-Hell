using Global.Camera;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum VariableName
    {
        NoVariables,
        AutomaticGunCountBullets,
        AutomaticGunCooldownTime,
        AutomaticGunShootingRate,
        AutomaticBulletDamage,
        AutomaticBulletSpeed,
        ShotGunCountBullets,
        ShotGunCooldownTime,
        ShotGunShootingRate,
        ShotGunAngleBullet,
        ShotGunCountBulletsPerShoot,
        ShotGunBulletDamage,
        ShotGunBulletSpeed,
        RocketLaucherCountBullets,
        RocketLaucherCooldownTime,
        RocketLaucherShootingRate,
        RocketLaucherBulletDamage,
        RocketLaucherBulletMinSpeed,
        RocketLaucherBulletMaxSpeed,
        RocketLaucherBulletAccelerationTime,
        RocketLaucherBulleTimeToBlowUp,
        RocketLaucherBulleRadiusToBlowUp,
        SpawnItemDataTimeToSpawn,
        SpawnItemDataTimeToHideWeaponAfterSpawn,
        PlayerSpeed,
        StartWeaponType,
        StartCameraType,
        CameraDistance,
        PauseTime
    }

    [Serializable]
    public class DynamicData
    {
        #region private variables

#pragma warning disable
        [SerializeField] private WeaponType startPlayerWeapon;
        [SerializeField] private SpawnItemData spawnItemData;
        [SerializeField] private RocketData rocketData;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private ShotgunData shotgunData;
        [SerializeField] private PauseData pauseData;

        [SerializeField]
        private Dictionary<VariableName, Action<string>> values = new Dictionary<VariableName, Action<string>>();

        [SerializeField] private GameCameraType cameraType;
        [SerializeField] private CameraData cameraData;
#pragma warning restore

        #endregion private variables

        #region properties

        public SpawnItemData SpawnItemData => spawnItemData;
        public RocketData RocketData => rocketData;
        public PlayerData PlayerData => playerData;
        public CameraData CameraData => cameraData;
        public ShotgunData ShotgunData => shotgunData;
        public PauseData PauseData => pauseData;
        public WeaponType StartPlayerWeapon => startPlayerWeapon;
        public GameCameraType StartCameraType => cameraType;

        #endregion properties

        #region public void

        //rewrite to few many functions
        public void SetValueToData(VariableName variableName, string value)
        {
            values[variableName]?.Invoke(value);
        }

        public void SetStartPlayerWeapon(WeaponType weaponType)
        {
            startPlayerWeapon = weaponType;
        }

        public void SetStartCameraType(GameCameraType cameraType)
        {
            this.cameraType = cameraType;
        }

        public void SetActionsToDictionary()
        {
            values.Add(VariableName.AutomaticGunCountBullets, (string value) => SetValueAutomatiGunCountBullets(value));
            values.Add(VariableName.AutomaticGunCooldownTime, (string value) => SetValueAutomaticGunCooldownTime(value));
            values.Add(VariableName.AutomaticGunShootingRate, (string value) => SetValueAutomaticGunShootingRate(value));
            values.Add(VariableName.AutomaticBulletDamage, (string value) => SetValueAutomaticBulletDamage(value));
            values.Add(VariableName.AutomaticBulletSpeed, (string value) => SetValueAutomaticBulletSpeed(value));
            values.Add(VariableName.ShotGunCountBullets, (string value) => SetValueShotGunCountBullets(value));
            values.Add(VariableName.ShotGunCooldownTime, (string value) => SetValueShotGunCooldownTime(value));
            values.Add(VariableName.ShotGunShootingRate, (string value) => SetValueShotGunShootingRate(value));
            values.Add(VariableName.ShotGunBulletDamage, (string value) => SetValueShotGunBulletDamage(value));
            values.Add(VariableName.ShotGunBulletSpeed, (string value) => SetValueShotGunBulletSpeed(value));
            values.Add(VariableName.ShotGunAngleBullet, (string value) => SetValueShotGunAngleBullet(value));
            values.Add(VariableName.ShotGunCountBulletsPerShoot, (string value) => SetValueShotGunCountBulletsPerShoot(value));
            values.Add(VariableName.RocketLaucherCountBullets, (string value) => SetValueRocketLaucherCountBullets(value));
            values.Add(VariableName.RocketLaucherCooldownTime, (string value) => SetValueRocketLaucherCooldownTime(value));
            values.Add(VariableName.RocketLaucherShootingRate, (string value) => SetValueRocketLaucherShootingRate(value));
            values.Add(VariableName.RocketLaucherBulletDamage, (string value) => SetValueRocketLaucherBulletDamage(value));
            values.Add(VariableName.RocketLaucherBulletMinSpeed, (string value) => SetValueRocketLaucherBulletMinSpeed(value));
            values.Add(VariableName.RocketLaucherBulletMaxSpeed, (string value) => SetValueRocketLaucherBulletMaxSpeed(value));
            values.Add(VariableName.RocketLaucherBulletAccelerationTime, (string value) => SetValueRocketLaucherBulletAccelerationTime(value));
            values.Add(VariableName.RocketLaucherBulleRadiusToBlowUp, (string value) => SetValueRocketLaucherBulleRadiusToBlowUp(value));
            values.Add(VariableName.RocketLaucherBulleTimeToBlowUp, (string value) => SetValueRocketLaucherBulleTimeToBlowUp(value));
            values.Add(VariableName.CameraDistance, (string value) => SetValueCameraDistance(value));
            values.Add(VariableName.PlayerSpeed, (string value) => SetValuePlayerSpeed(value));
            values.Add(VariableName.PauseTime, (string value) => SetValuePauseTime(value));
            values.Add(VariableName.SpawnItemDataTimeToSpawn, (string value) => SetValueSpawnItemDataTimeToSpawn(value));
            values.Add(VariableName.SpawnItemDataTimeToHideWeaponAfterSpawn, (string value) => SetValueSpawnItemDataTimeToHideWeaponAfterSpawn(value));
        }

        #endregion public void

        #region private void

        private void SetValueAutomatiGunCountBullets(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.AutomaticGun).bulletCount = int.Parse(value);
        }

        private void SetValueAutomaticGunCooldownTime(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.AutomaticGun).cooldownTime = float.Parse(value);
        }

        private void SetValueAutomaticGunShootingRate(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.AutomaticGun).shootingRate = float.Parse(value);
        }

        private void SetValueAutomaticBulletDamage(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetBulletStatsByType(BulletType.AutomaticBullet).damage = int.Parse(value);
        }

        private void SetValueAutomaticBulletSpeed(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetBulletStatsByType(BulletType.AutomaticBullet).speed = float.Parse(value);
        }

        private void SetValueShotGunCountBullets(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.Shotgun).bulletCount = int.Parse(value);
        }

        private void SetValueShotGunCooldownTime(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.Shotgun).cooldownTime = float.Parse(value);
        }

        private void SetValueShotGunShootingRate(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.Shotgun).shootingRate = float.Parse(value);
        }

        private void SetValueShotGunAngleBullet(string value)
        {
            Services.GetManager<DataManager>().DynamicData.ShotgunData.angleBullets = float.Parse(value);
        }

        private void SetValueShotGunCountBulletsPerShoot(string value)
        {
            Services.GetManager<DataManager>().DynamicData.ShotgunData.countBulletsInOnceShoot = int.Parse(value);
        }

        private void SetValueShotGunBulletDamage(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetBulletStatsByType(BulletType.ShotgunBullet).damage = int.Parse(value);
        }

        private void SetValueShotGunBulletSpeed(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetBulletStatsByType(BulletType.ShotgunBullet).speed = float.Parse(value);
        }

        private void SetValueRocketLaucherCountBullets(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.RocketLaucher).bulletCount = int.Parse(value);
        }

        private void SetValueRocketLaucherCooldownTime(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.RocketLaucher).cooldownTime = float.Parse(value);
        }

        private void SetValueRocketLaucherShootingRate(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetWeaponStatsByType(WeaponType.RocketLaucher).shootingRate = float.Parse(value);
        }

        private void SetValueRocketLaucherBulletDamage(string value)
        {
            Services.GetManager<DataManager>().StaticData.GetBulletStatsByType(BulletType.RocketLaucherBullet).damage = int.Parse(value);
        }

        private void SetValueRocketLaucherBulletMinSpeed(string value)
        {
            rocketData.minSpeed = float.Parse(value);
        }

        private void SetValueRocketLaucherBulletMaxSpeed(string value)
        {
            rocketData.maxSpeed = float.Parse(value);
        }

        private void SetValueRocketLaucherBulletAccelerationTime(string value)
        {
            rocketData.timeAcceleration = float.Parse(value);
        }

        private void SetValueRocketLaucherBulleTimeToBlowUp(string value)
        {
            rocketData.timeToBlowUp = int.Parse(value);
        }

        private void SetValueRocketLaucherBulleRadiusToBlowUp(string value)
        {
            rocketData.radiusBlowUp = float.Parse(value);
        }

        private void SetValueSpawnItemDataTimeToSpawn(string value)
        {
            spawnItemData.spawnTime = int.Parse(value);
        }

        private void SetValueSpawnItemDataTimeToHideWeaponAfterSpawn(string value)
        {
            spawnItemData.destroyTime = int.Parse(value);
        }

        private void SetValuePlayerSpeed(string value)
        {
            playerData.speed = float.Parse(value);
        }

        private void SetValueCameraDistance(string value)
        {
            cameraData.cameraDistance = float.Parse(value);
        }

        private void SetValuePauseTime(string value)
        {
            pauseData.pauseTime = float.Parse(value);
        }

        #endregion private void
    }

    [Serializable]
    public class CameraData
    {
        public float cameraDistance;
    }

    [Serializable]
    public class SpawnItemData
    {
        public int spawnTime;
        public int destroyTime;
    }

    [Serializable]
    public class RocketData
    {
        public int timeToBlowUp;
        public float radiusBlowUp;
        public float minSpeed;
        public float maxSpeed;
        public float timeAcceleration;
    }

    [Serializable]
    public class ShotgunData
    {
        public float angleBullets;
        public int countBulletsInOnceShoot;
    }

    [Serializable]
    public class PlayerData
    {
        public int hp;
        public float speed;
        public int defence;
    }

    [Serializable]
    public class PauseData
    {
        public float pauseTime;
    }
}