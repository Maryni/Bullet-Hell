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
        [SerializeField] private Transform transformPlayer;
        [SerializeField] private Rigidbody2D rig2d;
        [SerializeField] private float rateMovement;
        [SerializeField] private float rateReset;
        [SerializeField] private bool movementEnable = true;
        [SerializeField] private bool resetEnable = true;
        [SerializeField] private EnemyMovement enemyMovement;

#pragma warning restore

        #endregion Inspector variables

        #region properties

        public EnemyType EnemyType => enemyType;

        #endregion properties

        #region Unity functions

        private void Start()
        {
            Init(enemyType);
            StartCoroutine(SetPlayerTransform());
        }

        private void OnValidate()
        {
            rig2d = GetComponent<Rigidbody2D>();
            enemyMovement = GetComponent<EnemyMovement>();
        }

        #endregion Unity functions

        #region public void

        public override void Movement()
        {
            StartCoroutine(ResetVelocity());
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

        private IEnumerator SetPlayerTransform()
        {
            if (transformPlayer == null)
            {
                transformPlayer = FindObjectOfType<Player.Player>().transform;
            }
            yield return new WaitForEndOfFrame();
            SetPlayerTransform();
        }

        private IEnumerator ResetVelocity()
        {
            if (resetEnable)
            {
                rig2d.velocity = Vector2.zero;
                yield return new WaitForSeconds(rateReset);
            }
            ResetVelocity();
        }

        private Vector2 Rotation(Transform transformObject)
        {
            return ((Vector2)transformPlayer.position - (Vector2)transformObject.position).normalized;
        }

        private IEnumerator Move()
        {
            if (movementEnable)
            {
                if (transformPlayer != null)
                {
                    transform.up = Rotation(transform);
                    enemyMovement.Movement(transformPlayer, rig2d, EnemyStats.speed);
                }
                yield return new WaitForSeconds(rateMovement);
            }
            Move();
        }

        #endregion private void
    }
}