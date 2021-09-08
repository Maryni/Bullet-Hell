using Global.Camera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum VariableName
    {
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
        SpawnWeaponDataTime,
        DispawnWeaponDataTime,
        SpawnPowerUpDataTime,
        DispawnPowerUpDataTime,
        DurationPowerUpOnPlayer,
        KillAreaRadiusPowerUp,
        PlayerHp,
        PlayerSpeed,
        CameraDistance,
        PauseTime,
        SlowEnemySpeed,
        MiddleEnemySpeed,
        FastEnemySpeed,
        StartWeapon,
        StartCameraType
    }

    [Serializable]
    public class DynamicData
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private float powerUpModifier;

        [Header("Weapon Data"), SerializeField] private WeaponType startPlayerWeapon;
        [SerializeField] private RocketData rocketData;
        [SerializeField] private ShotgunData shotgunData;

        [Header("Weapon & PowerUp Settings"), SerializeField] private SpawnWeaponData weaponSpawnItemData;
        [SerializeField] private SpawnPowerUpData powerUpSpawnItemData;

        [Header("Player Data"), SerializeField] private PlayerData playerData;

        [Header("Game Settings"), SerializeField] private GameCameraType cameraType;
        [SerializeField] private PauseData pauseData;
        [SerializeField] private CameraData cameraData;

        [Header("Enemy Data"), SerializeField] private EnemyData meleeSlowEnemyData;
        [SerializeField] private EnemyData meleeMiddleEnemyData;
        [SerializeField] private EnemyData meleeFastEnemyData;

        [Header("Bullet Data"), SerializeField] private BulletData automaticBulletData;
        [SerializeField] private BulletData shotgunBulletData;
        [SerializeField] private BulletData rocketBulletData;

        [Header("Weapon Data"), SerializeField] private WeaponData automaticWeaponsData;
        [SerializeField] private WeaponData shotgunWeaponData;
        [SerializeField] private WeaponData rocketLaucherWeaponData;

#pragma warning restore

        #endregion Inspector variables

        #region properties

        public SpawnPowerUpData PowerUpSpawnItemData => powerUpSpawnItemData;
        public SpawnWeaponData WeaponSpawnItemData => weaponSpawnItemData;
        public RocketData RocketData => rocketData;
        public PlayerData PlayerData => playerData;
        public CameraData CameraData => cameraData;
        public ShotgunData ShotgunData => shotgunData;
        public PauseData PauseData => pauseData;
        public WeaponType StartPlayerWeapon => startPlayerWeapon;
        public GameCameraType StartCameraType => cameraType;

        #endregion properties

        #region private variables

        private Dictionary<VariableName, Action<string>> values = new Dictionary<VariableName, Action<string>>();
        private Dictionary<TypePowerUp, Action<bool>> powerUpFunctions = new Dictionary<TypePowerUp, Action<bool>>();

        #endregion private variables

        #region public void

        public EnemyData GetEnemyStatsByType(EnemyType enemyType)
        {
            if (enemyType == EnemyType.MeleeGrounded_FastSpeed)
            {
                return meleeFastEnemyData;
            }
            else if (enemyType == EnemyType.MeleeGrounded_LowSpeed)
            {
                return meleeSlowEnemyData;
            }
            else
            {
                return meleeMiddleEnemyData;
            }
        }

        public BulletData GetBulletDataByType(BulletType bulletType)
        {
            if (bulletType == BulletType.AutomaticBullet)
            {
                return automaticBulletData;
            }
            else if (bulletType == BulletType.ShotgunBullet)
            {
                return shotgunBulletData;
            }
            else
            {
                return rocketBulletData;
            }
        }

        public WeaponData GetWeaponDataByType(WeaponType weaponType)
        {
            if (weaponType == WeaponType.AutomaticGun)
            {
                return automaticWeaponsData;
            }
            else if (weaponType == WeaponType.Shotgun)
            {
                return shotgunWeaponData;
            }
            else
            {
                return rocketLaucherWeaponData;
            }
        }

        public void SetValueToData(VariableName variableName, string value)
        {
            values[variableName]?.Invoke(value);
        }

        public void EnablesPowerUpByType(TypePowerUp typePowerUp, bool value)
        {
            powerUpFunctions[typePowerUp]?.Invoke(value);
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
            values.Add(VariableName.PlayerHp, (string value) => SetValuePlayerHp(value));
            values.Add(VariableName.PlayerSpeed, (string value) => SetValuePlayerSpeed(value));
            values.Add(VariableName.PauseTime, (string value) => SetValuePauseTime(value));
            values.Add(VariableName.SpawnWeaponDataTime, (string value) => SetValueSpawnWeaponDataTime(value));
            values.Add(VariableName.DispawnWeaponDataTime, (string value) => SetValueDispawnWeaponDataTime(value));
            values.Add(VariableName.SpawnPowerUpDataTime, (string value) => SetValueSpawnPowerUpDataTime(value));
            values.Add(VariableName.DispawnPowerUpDataTime, (string value) => SetValueDispawnPowerUpDataTime(value));
            values.Add(VariableName.DurationPowerUpOnPlayer, (string value) => SetValueDurationPowerUpDataTime(value));
            values.Add(VariableName.KillAreaRadiusPowerUp, (string value) => SetKillAreaRadiusPowerUp(value));
            values.Add(VariableName.SlowEnemySpeed, (string value) => SetValueSlowEnemySpeed(value));
            values.Add(VariableName.MiddleEnemySpeed, (string value) => SetValueMiddleEnemySpeed(value));
            values.Add(VariableName.FastEnemySpeed, (string value) => SetValueFastEnemySpeed(value));
            values.Add(VariableName.StartWeapon, (string value) => SetStartWeapon(value));
            values.Add(VariableName.StartCameraType, (string value) => SetStartCameraType(value));

            powerUpFunctions.Add(TypePowerUp.IncreaseDamage, (bool value) => DamagePowerUp(value));
            powerUpFunctions.Add(TypePowerUp.IncreaseSpeed, (bool value) => SpeedPowerUp(value));
        }

        #endregion public void

        #region private void

        #region Dictinary functions

        #region Data Functions

        private void SetStartWeapon(string value)
        {
            WeaponType weaponType = WeaponType.AutomaticGun;
            if (value == WeaponType.Shotgun.ToString())
            {
                weaponType = WeaponType.Shotgun;
            }
            if (value == WeaponType.RocketLaucher.ToString())
            {
                weaponType = WeaponType.RocketLaucher;
            }

            startPlayerWeapon = weaponType;
        }

        private void SetStartCameraType(string value)
        {
            GameCameraType cameraType = GameCameraType.DynamicCamera;
            if (value == GameCameraType.StaticCamera.ToString())
            {
                cameraType = GameCameraType.StaticCamera;
            }
            this.cameraType = cameraType;
        }

        private void SetValueAutomatiGunCountBullets(string value)
        {
            GetWeaponDataByType(WeaponType.AutomaticGun).bulletCount = int.Parse(value);
        }

        private void SetValueAutomaticGunCooldownTime(string value)
        {
            GetWeaponDataByType(WeaponType.AutomaticGun).cooldownTime = float.Parse(value);
        }

        private void SetValueAutomaticGunShootingRate(string value)
        {
            GetWeaponDataByType(WeaponType.AutomaticGun).shootingRate = float.Parse(value);
        }

        private void SetValueAutomaticBulletDamage(string value)
        {
            GetBulletDataByType(BulletType.AutomaticBullet).damage = int.Parse(value);
        }

        private void SetValueAutomaticBulletSpeed(string value)
        {
            GetBulletDataByType(BulletType.AutomaticBullet).speed = float.Parse(value);
        }

        private void SetValueShotGunCountBullets(string value)
        {
            GetWeaponDataByType(WeaponType.Shotgun).bulletCount = int.Parse(value);
        }

        private void SetValueShotGunCooldownTime(string value)
        {
            GetWeaponDataByType(WeaponType.Shotgun).cooldownTime = float.Parse(value);
        }

        private void SetValueShotGunShootingRate(string value)
        {
            GetWeaponDataByType(WeaponType.Shotgun).shootingRate = float.Parse(value);
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
            GetBulletDataByType(BulletType.ShotgunBullet).damage = int.Parse(value);
        }

        private void SetValueShotGunBulletSpeed(string value)
        {
            GetBulletDataByType(BulletType.ShotgunBullet).speed = float.Parse(value);
        }

        private void SetValueRocketLaucherCountBullets(string value)
        {
            GetWeaponDataByType(WeaponType.RocketLaucher).bulletCount = int.Parse(value);
        }

        private void SetValueRocketLaucherCooldownTime(string value)
        {
            GetWeaponDataByType(WeaponType.RocketLaucher).cooldownTime = float.Parse(value);
        }

        private void SetValueRocketLaucherShootingRate(string value)
        {
            GetWeaponDataByType(WeaponType.RocketLaucher).shootingRate = float.Parse(value);
        }

        private void SetValueRocketLaucherBulletDamage(string value)
        {
            GetBulletDataByType(BulletType.RocketLaucherBullet).damage = int.Parse(value);
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

        private void SetValueSpawnWeaponDataTime(string value)
        {
            weaponSpawnItemData.spawnTime = int.Parse(value);
        }

        private void SetValueDispawnWeaponDataTime(string value)
        {
            weaponSpawnItemData.destroyTime = int.Parse(value);
        }

        private void SetValueSpawnPowerUpDataTime(string value)
        {
            powerUpSpawnItemData.spawnTime = int.Parse(value);
        }

        private void SetValueDispawnPowerUpDataTime(string value)
        {
            powerUpSpawnItemData.destroyTime = int.Parse(value);
        }

        private void SetValueDurationPowerUpDataTime(string value)
        {
            powerUpSpawnItemData.duration = float.Parse(value);
        }

        private void SetKillAreaRadiusPowerUp(string value)
        {
            powerUpSpawnItemData.killAreaRadius = float.Parse(value);
        }

        private void SetValuePlayerHp(string value)
        {
            playerData.hp = int.Parse(value);
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

        private void SetValueSlowEnemySpeed(string value)
        {
            meleeSlowEnemyData.speed = float.Parse(value);
        }

        private void SetValueMiddleEnemySpeed(string value)
        {
            meleeMiddleEnemyData.speed = float.Parse(value);
        }

        private void SetValueFastEnemySpeed(string value)
        {
            meleeFastEnemyData.speed = float.Parse(value);
        }

        #endregion Data Functions

        #region PowerUp

        private void DamagePowerUp(bool value)
        {
            if (value)
            {
                automaticBulletData.damage = (int)(automaticBulletData.damage * powerUpModifier);
                shotgunBulletData.damage = (int)(shotgunBulletData.damage * powerUpModifier);
                rocketBulletData.damage = (int)(rocketBulletData.damage * powerUpModifier);
            }
            else
            {
                automaticBulletData.damage = (int)(automaticBulletData.damage / powerUpModifier);
                shotgunBulletData.damage = (int)(shotgunBulletData.damage / powerUpModifier);
                rocketBulletData.damage = (int)(rocketBulletData.damage / powerUpModifier);
            }
        }

        private void SpeedPowerUp(bool value)
        {
            if (value)
            {
                playerData.speed *= powerUpModifier;
            }
            else
            {
                playerData.speed /= powerUpModifier;
            }
        }

        #endregion PowerUp

        #endregion Dictinary functions

        #endregion private void
    }

    [Serializable]
    public class CameraData
    {
        public float cameraDistance;
    }

    [Serializable]
    public class SpawnWeaponData
    {
        public int spawnTime;
        public int destroyTime;
    }

    [Serializable]
    public class SpawnPowerUpData
    {
        public int spawnTime;
        public int destroyTime;
        public float duration;
        public float killAreaRadius;
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

    [Serializable]
    public class EnemyData
    {
        public EnemyType enemyType;
        public int hpMaximum;
        public float speed;
        public float hpValueCurrent;
        public float damage;
        public int defence;
        public int intelligence;
        public float attackRate;
    }

    [Serializable]
    public class BulletData
    {
        public BulletType bulletType;
        public int damage;
        public float speed;
    }

    [Serializable]
    public class WeaponData
    {
        public WeaponType weaponType;
        public int bulletCount;
        public float cooldownTime;
        public float shootingRate;
    }
}