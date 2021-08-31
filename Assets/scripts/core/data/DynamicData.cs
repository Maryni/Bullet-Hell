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
        RocketLaucherBulletSpeed,
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
        [SerializeField] private Dictionary<VariableName, Action> values = new Dictionary<VariableName, Action>();
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

        public void SetValueToData(VariableName variableName, string value)
        {
            values.Add(variableName, () => SetValue(value, variableName));
            values[variableName]?.Invoke();
            values.Clear();
        }

        public void SetStartPlayerWeapon(WeaponType weaponType)
        {
            startPlayerWeapon = weaponType;
        }

        public void SetStartCameraType(GameCameraType cameraType)
        {
            this.cameraType = cameraType;
        }

        public void SetValue(string value, VariableName variableName)
        {
            var data = Services.GetManager<DataManager>();
            if (variableName == VariableName.AutomaticGunCountBullets)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.AutomaticGun).bulletCount = int.Parse(value);
            }
            if (variableName == VariableName.AutomaticGunCooldownTime)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.AutomaticGun).cooldownTime = float.Parse(value);
            }
            if (variableName == VariableName.AutomaticGunShootingRate)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.AutomaticGun).shootingRate = float.Parse(value);
            }
            if (variableName == VariableName.AutomaticBulletDamage)
            {
                data.StaticData.GetBulletStatsByType(BulletType.AutomaticBullet).damage = int.Parse(value);
            }
            if (variableName == VariableName.AutomaticBulletSpeed)
            {
                data.StaticData.GetBulletStatsByType(BulletType.AutomaticBullet).speed = float.Parse(value);
            }
            if (variableName == VariableName.ShotGunCountBullets)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.Shotgun).bulletCount = int.Parse(value);
            }
            if (variableName == VariableName.ShotGunCooldownTime)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.Shotgun).cooldownTime = float.Parse(value);
            }
            if (variableName == VariableName.ShotGunShootingRate)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.Shotgun).shootingRate = float.Parse(value);
            }
            if (variableName == VariableName.ShotGunAngleBullet)
            {
                data.DynamicData.ShotgunData.angleBullets = float.Parse(value);
            }
            if (variableName == VariableName.ShotGunCountBulletsPerShoot)
            {
                data.DynamicData.ShotgunData.countBulletsInOnceShoot = int.Parse(value);
            }
            if (variableName == VariableName.ShotGunBulletDamage)
            {
                data.StaticData.GetBulletStatsByType(BulletType.ShotgunBullet).damage = int.Parse(value);
            }
            if (variableName == VariableName.ShotGunBulletSpeed)
            {
                data.StaticData.GetBulletStatsByType(BulletType.ShotgunBullet).speed = float.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherCountBullets)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.RocketLaucher).bulletCount = int.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherCooldownTime)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.RocketLaucher).cooldownTime = float.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherShootingRate)
            {
                data.StaticData.GetWeaponStatsByType(WeaponType.RocketLaucher).shootingRate = float.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherBulletDamage)
            {
                data.StaticData.GetBulletStatsByType(BulletType.RocketLaucherBullet).damage = int.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherBulletSpeed)
            {
                data.StaticData.GetBulletStatsByType(BulletType.RocketLaucherBullet).speed = float.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherBulleTimeToBlowUp)
            {
                rocketData.timeToBlowUp = int.Parse(value);
            }
            if (variableName == VariableName.RocketLaucherBulleRadiusToBlowUp)
            {
                rocketData.radiusBlowUp = float.Parse(value);
            }
            if (variableName == VariableName.SpawnItemDataTimeToSpawn)
            {
                spawnItemData.spawnTime = int.Parse(value);
            }
            if (variableName == VariableName.SpawnItemDataTimeToHideWeaponAfterSpawn)
            {
                spawnItemData.destroyTime = int.Parse(value);
            }
            if (variableName == VariableName.PlayerSpeed)
            {
                playerData.speed = float.Parse(value);
            }
            if (variableName == VariableName.CameraDistance)
            {
                cameraData.cameraDistance = float.Parse(value);
            }
            if (variableName == VariableName.PauseTime)
            {
                pauseData.pauseTime = float.Parse(value);
            }
        }

        #endregion public void
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