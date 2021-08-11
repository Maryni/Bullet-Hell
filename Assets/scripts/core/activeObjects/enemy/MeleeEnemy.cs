using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Player;
using Global.Managers.Datas;

namespace Global.ActiveObjects
{
    public class MeleeEnemy : BaseEnemy
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] protected EnemyType enemyType;

        [SerializeField] private float rateMovement;
        [SerializeField] private float rateReset;
        [SerializeField] private bool movementEnable = true;
        [SerializeField] private bool resetEnable = true;
        [SerializeField] private Rigidbody2D rig2d;
        [SerializeField] private EnemyMovement enemyMovement;

#pragma warning restore

        #endregion Inspector variables

        #region properties

        public EnemyType EnemyType => enemyType;

        #endregion properties

        #region Unity functions

        private void Awake()
        {
            Init(enemyType);
        }

        private void OnValidate()
        {
            if (rig2d == null)
            {
                rig2d = GetComponent<Rigidbody2D>();
            }
            if (enemyMovement == null)
            {
                enemyMovement = GetComponent<EnemyMovement>();
            }
        }

        private void Start()
        {
            if (transformPlayer == null)
            {
                transformPlayer = FindObjectOfType<Player.Player>().transform;
            }
        }

        #endregion Unity functions

        #region public void

        public override void Movement()
        {
            StartCoroutine(Move());
        }

        public override void ObjectTriggered(int damage)
        {
            base.ObjectTriggered(damage);
            Debug.Log($"Im Enemy[ {name} ] triggered");
            EnemyStats.hpValueCurrent -= DamageTakenCalculator(damage);
            if (EnemyStats.hpValueCurrent <= 0)
            {
                Dead();
            }
        }

        public override void Dead()
        {
            transform.parent.gameObject.SetActive(false);
            EnemyStats.hpValueCurrent = EnemyStats.hpMaximum;
        }

        public override int DamageTakenCalculator(int damage)
        {
            var hpDecrese = damage - EnemyStats.defence;
            if (hpDecrese < 0)
            {
                hpDecrese = 0;
            }
            return hpDecrese;
        }

        #endregion public void

        #region private void

        private Vector2 Rotation(Transform transformObject)
        {
            return ((Vector2)transformPlayer.position - (Vector2)transformObject.position).normalized;
        }

        private IEnumerator Move()
        {
            Debug.Log(transformPlayer);
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