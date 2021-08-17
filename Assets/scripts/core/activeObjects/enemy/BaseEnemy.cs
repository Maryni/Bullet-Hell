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
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private EnemyStats enemyStatsData;

        #endregion private variables

        #region properties

        public EnemyStats EnemyStats => enemyStatsData;

        #endregion properties

        #region public void

        public virtual void Init(EnemyType enemyType)
        {
            enemyStatsData = Services.GetManager<DataManager>().StaticData.GetEnemyStatsByType(enemyType);
        }

        public abstract void ObjectTriggered(int damage); //GetDamage

        public abstract void ObjectTriggered(float damage); //GetDamage

        public void Movement()
        {
            StartCoroutine(Move());
        }

        #endregion public void

        #region protected void

        protected void Dead()
        {
            transform.parent.gameObject.SetActive(false);
            EnemyStats.hpValueCurrent = EnemyStats.hpMaximum;
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

        protected Vector2 Rotation(Transform transformObject)
        {
            return ((Vector2)transformPlayer.position - (Vector2)transformObject.position).normalized;
        }

        #endregion protected void

        #region private void

        private IEnumerator Move()
        {
            if (transformPlayer != null)
            {
                while (movementEnable)
                {
                    transform.up = Rotation(transform);
                    enemyMovement.Movement(transformPlayer, rig2d, EnemyStats.speed);

                    yield return null;
                }
            }
        }

        #endregion private void
    }
}