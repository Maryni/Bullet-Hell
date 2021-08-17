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
            EnemyStats.hpValueCurrent = EnemyStats.hpMaximum;
        }

        #endregion Unity functions

        #region public void

        public override void ObjectTriggered(int damage)
        {
            Debug.Log($"Im Enemy[ {name} ] triggered, and damage = " + damage + "\t and hp = " + EnemyStats.hpValueCurrent);
            EnemyStats.hpValueCurrent -= DamageTakenCalculator(damage);
            if (EnemyStats.hpValueCurrent <= 0)
            {
                Dead();
            }
        }

        public override void ObjectTriggered(float damage)
        {
            Debug.Log($"Im Enemy[ {name} ] triggered, and damage = " + damage + "\t and hp = " + EnemyStats.hpValueCurrent);
            EnemyStats.hpValueCurrent -= DamageTakenCalculator(damage);
            if (EnemyStats.hpValueCurrent <= 0)
            {
                Dead();
            }
        }

        #endregion public void
    }
}