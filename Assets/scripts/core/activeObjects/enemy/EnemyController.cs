using Global.Managers.Datas;
using Global.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class EnemyController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private MeleeEnemy meleeEnemy;
        [SerializeField] private PlayerTriggerChecker enemyTrigger;
        [SerializeField] private MeleeAttack meleeAttack;

        #endregion Inspector variables

        #region properties

        public MeleeEnemy MeleeEnemy => meleeEnemy;

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
            meleeEnemy.Movement();
            EnableAttack();
            StartCoroutine(meleeAttack.Attack(meleeEnemy.EnemyType));
        }

        #endregion Unity functions

        #region public void

        public void EnableAttack()
        {
            enemyTrigger.EnableAttack();
        }

        #endregion public void
    }
}