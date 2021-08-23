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

        public abstract void ObjectTriggered(int damage); //измени нейминг

        public abstract void ObjectTriggered(float damage); //лучше сделай только через флоат

        public void Movement()
        {
            StartCoroutine(Move());
        }

        #endregion public void

        #region protected void

        protected void Dead()
        {
            gameObject.SetActive(false);
            ResetEnemyHP(); //зачем ты ресетишь хп, когда энеми помер? Тебе нужно это делать при иницализации
                            //если бы мы умирали и сразу получали хп - мы бы жили вечно :)
        }

        protected int DamageTakenCalculator(int damage)
        {
            var hpDecrese = damage - EnemyStats.defence; //нейминг
            if (hpDecrese < 0)
            {
                hpDecrese = 0;
            }
            return hpDecrese;
        }

        protected float DamageTakenCalculator(float damage)
        {
            var hpDecrese = damage - EnemyStats.defence; //нейминг
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

        //у тебя есть скрипт ЕнемиМувмент, почему бейс енеми знает о движении?
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