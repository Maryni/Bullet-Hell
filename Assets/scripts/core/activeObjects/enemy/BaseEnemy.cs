using System.Collections;
using UnityEngine;
using Global.Managers.Datas;
using System;

namespace Global.ActiveObjects
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] protected Transform transformPlayer;
        [SerializeField] protected Rigidbody2D rig2d;
        [SerializeField] protected EnemyType enemyType;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private EnemyStats enemyStatsData;
        private Action actionOnDie;

        #endregion private variables

        #region properties

        public EnemyStats EnemyStats => enemyStatsData;
        public EnemyType EnemyType => enemyType;
        public Transform TransformPlayer => transformPlayer;
        public Rigidbody2D Rig2D => rig2d;

        #endregion properties

        #region public void

        public void AddEventOnDie(Action action)
        {
            actionOnDie += action;
        }

        public void SetTransformPlayer()
        {
            if (transformPlayer == null)
            {
                transformPlayer = FindObjectOfType<Player.Player>().transform;
            }
        }

        public void ResetEnemyHP()
        {
            EnemyStats.hpValueCurrent = EnemyStats.hpMaximum;
        }

        public virtual void Init(EnemyType enemyType)
        {
            enemyStatsData = Services.GetManager<DataManager>().StaticData.GetEnemyStatsByType(enemyType);
        }

        public abstract void GetDamage(float damage);

        #endregion public void

        #region protected void

        protected void Dead()
        {
            gameObject.SetActive(false);
            actionOnDie?.Invoke();
        }

        protected int DamageTakenCalculator(int damage)
        {
            var hpDecrease = damage - EnemyStats.defence;
            if (hpDecrease < 0)
            {
                hpDecrease = 0;
            }
            return hpDecrease;
        }

        protected float DamageTakenCalculator(float damage)
        {
            var hpDecrease = damage - EnemyStats.defence;
            if (hpDecrease < 0)
            {
                hpDecrease = 0;
            }
            return hpDecrease;
        }

        #endregion protected void
    }
}