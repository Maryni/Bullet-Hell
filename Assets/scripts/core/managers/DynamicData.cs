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
        [SerializeField] private PlayerData playerData;
        [SerializeField] private SpawnItemData spawnItemData;
        [SerializeField] private RocketData rocketData;
        [SerializeField] private EnemyStats enemyStatsData;
#pragma warning restore

        #endregion private variables

        #region properties

        public PlayerData PlayerData => playerData;
        public SpawnItemData SpawnItemData => spawnItemData;
        public RocketData RocketData => rocketData;
        public EnemyStats EnemyStats => enemyStatsData;

        #endregion properties
    }

    [Serializable]
    public class PlayerData
    {
        public int hpMaximum;
        public int speed;
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
}