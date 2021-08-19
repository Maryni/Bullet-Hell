using Global.Camera;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum VariableName
    {
        NoVariables,
        RocketDataTimeToBlowUp,
        RocketDataRadiusToBlowUp,
        SpawnItemDataTimeToSpawn,
        SpawnItemDataTimeToHideWeaponAfterSpawn,
        PlayerSpeed
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
        [SerializeField] private Dictionary<VariableName, Action> values = new Dictionary<VariableName, Action>();
        [SerializeField] private GameCameraType cameraType;
#pragma warning restore

        #endregion private variables

        #region properties

        public SpawnItemData SpawnItemData => spawnItemData;
        public RocketData RocketData => rocketData;
        public PlayerData PlayerData => playerData;
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
            if (variableName == VariableName.RocketDataTimeToBlowUp)
            {
                rocketData.timeToBlowUp = int.Parse(value);
            }
            if (variableName == VariableName.RocketDataRadiusToBlowUp)
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
                playerData.speed = int.Parse(value);
            }
        }

        #endregion public void
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
    public class BulletStats
    {
        //TODO: move BulletStats to ScriptableObjects, add bulletStats to necessaryWeapon
        public int damage;

        public int speed;
    }

    [Serializable]
    public class PlayerData
    {
        public int hp;
        public int speed;
        public int defence;
    }
}