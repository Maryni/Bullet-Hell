using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Settings;
using Global.Interfaces;
using System;
using Global.Managers.Datas;

namespace Global.Managers.Datas
{
    [Serializable]
    public class EnemyStats
    {
        [SerializeField] public int hp;
        [SerializeField] public float speed;
        [SerializeField] public int hpValue;
        [SerializeField] public float damage;
        [SerializeField] public int intellegence;
        [SerializeField] private WeaponStats weaponStats;
    }
}

namespace Global.ActiveObjects
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private EnemyStats enemyStatsData;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public EnemyStats EnemyStats => enemyStatsData;

        #endregion properties

        #region public void

        public virtual void Init()
        {
            enemyStatsData = Services.GetManager<DataManager>().DynamicData.EnemyStats;
        }

        public abstract void Movement();

        public abstract void Attack();

        #endregion public void
    }
}