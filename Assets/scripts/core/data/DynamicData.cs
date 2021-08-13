using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [Serializable]
    public class DynamicData
    {
        #region private variables

#pragma warning disable
        [SerializeField] private WeaponType startPlayerWeapon;
        [SerializeField] private SpawnItemData spawnItemData;
        [SerializeField] private RocketData rocketData;
#pragma warning restore

        #endregion private variables

        #region properties

        public SpawnItemData SpawnItemData => spawnItemData;
        public RocketData RocketData => rocketData;
        public WeaponType StartPlayerWeapon => startPlayerWeapon;

        #endregion properties

        public void SetStartPlayerWeapon(WeaponType weaponType)
        {
            startPlayerWeapon = weaponType;
        }
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
        public float radiutBlowUp;
    }

    [Serializable]
    public class BulletStats
    {
        //TODO: move BulletStats to ScriptableObjects, add bulletStats to nessasaryWeapon
        public int damage;

        public int speed;
    }
}