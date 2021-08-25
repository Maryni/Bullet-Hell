using System.Collections;
using UnityEngine;
using Global.Managers.Datas;

namespace Global.ActiveObjects
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] protected Transform transformPlayer;
        [SerializeField] protected EnemyMovement enemyMovement;
        [SerializeField] protected bool movementEnable = true;
        [SerializeField] protected Rigidbody2D rig2d;
        [SerializeField] protected EnemyType enemyType;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private EnemyStats enemyStatsData;

        #endregion private variables

        #region properties

        public EnemyStats EnemyStats => enemyStatsData;
        public EnemyType EnemyType => enemyType;
        public Transform TransformPlayer => transformPlayer;
        public Rigidbody2D Rig2D => rig2d;

        #endregion properties

        #region public void

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
        }

        protected int DamageTakenCalculator(int damage)
        {
            var hpDecrese = damage - EnemyStats.defence;
            if (hpDecrese < 0)
            {
                hpDecrese = 0;
            }
            return hpDecrese;
        }

        protected float DamageTakenCalculator(float damage)
        {
            var hpDecrese = damage - EnemyStats.defence;
            if (hpDecrese < 0)
            {
                hpDecrese = 0;
            }
            return hpDecrese;
        }

        #endregion protected void
    }
}