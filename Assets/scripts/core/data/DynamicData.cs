using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum VariableName
    {
        NoVariables,
        RocketDataTimeToBlowUp,
        RocketDataRadiusToBlowUp
    }

    [Serializable]
    public class DynamicData
    {
        #region private variables

#pragma warning disable
        [SerializeField] private WeaponType startPlayerWeapon;
        [SerializeField] private SpawnItemData spawnItemData;
        [SerializeField] private RocketData rocketData;
        [SerializeField] private Dictionary<VariableName, Action> values = new Dictionary<VariableName, Action>();
#pragma warning restore

        #endregion private variables

        #region properties

        public SpawnItemData SpawnItemData => spawnItemData;
        public RocketData RocketData => rocketData;
        public WeaponType StartPlayerWeapon => startPlayerWeapon;

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
}