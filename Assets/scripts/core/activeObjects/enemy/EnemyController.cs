﻿using Global.Managers.Datas;
using Global.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class EnemyController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private BaseEnemy meleeEnemy;
        [SerializeField] private PlayerTriggerChecker enemyTrigger;
        [SerializeField] private MeleeAttack meleeAttack;

        #endregion Inspector variables

        #region properties

        public MeleeEnemy MeleeEnemy => (MeleeEnemy)meleeEnemy;

        #endregion properties

        #region Unity functions

        private void OnValidate()
        {
            enemyTrigger = GetComponentInChildren<PlayerTriggerChecker>();
            meleeEnemy = GetComponent<MeleeEnemy>();
            meleeAttack = GetComponentInChildren<MeleeAttack>();
        }

        private void Start()
        {
            ActivateEnemy();
        }

        #endregion Unity functions

        #region public void

        public void ActivateEnemy()
        {
            meleeEnemy.Movement();
            EnableAttack();
            StartCoroutine(meleeAttack.Attack(meleeEnemy.EnemyStats.enemyType));
        }

        public void EnableAttack()
        {
            enemyTrigger.EnableAttack();
        }

        #endregion public void
    }
}