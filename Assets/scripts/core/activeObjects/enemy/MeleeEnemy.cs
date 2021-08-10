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
        [SerializeField] private float timeInvokeReapeating;
        [SerializeField] private float rateInvokeReapeatingMovement;
        [SerializeField] private float rateInvokeReapeatingReset;
        [SerializeField] private EnemyMovement enemyMovement;

#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init(enemyType);
            transformPlayer = FindObjectOfType<Player.Player>().transform;
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
            InvokeRepeating("ResetVelocity", timeInvokeReapeating, rateInvokeReapeatingReset);
            InvokeRepeating("Move", timeInvokeReapeating, rateInvokeReapeatingMovement);
        }

        public override void ObjectTriggered()
        {
            base.ObjectTriggered();
            Debug.Log($"Im Enemy[ {name} ] triggered");
            transform.parent.gameObject.SetActive(false);
        }

        #endregion public void

        #region private void

        private void ResetVelocity()
        {
            rig2d.velocity = Vector2.zero;
        }

        private Vector2 Rotation(Transform transformObject)
        {
            return ((Vector2)transformPlayer.position - (Vector2)transformObject.position).normalized;
        }

        private void Move()
        {
            transform.up = Rotation(transform);
            enemyMovement.Movement(transformPlayer, rig2d, EnemyStats.speed);
        }

        #endregion private void
    }
}