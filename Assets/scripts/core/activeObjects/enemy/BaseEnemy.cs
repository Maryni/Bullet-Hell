using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Settings;
using Global.Interfaces;
using System;
using Global.Managers.Datas;

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

        public virtual void Init(EnemyType enemyType)
        {
            enemyStatsData = Services.GetManager<DataManager>().StaticData.GetEnemyStatsByType(enemyType);
        }

        public virtual void ObjectTriggered()
        {
            StopCoroutine("ObjectTriggered");
        }

        public abstract void Movement();

        #endregion public void
    }
}