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

        #endregion Inspector variables

        #region properties

        public MeleeEnemy MeleeEnemy => meleeEnemy;

        #endregion properties

        #region Unity functions

        private void Start()
        {
            enemyTrigger = GetComponentInChildren<PlayerTriggerChecker>();
            meleeEnemy = GetComponent<MeleeEnemy>();
            meleeEnemy.Movement();
            EnableAttack();
        }

        #endregion Unity functions

        #region public void

        public void EnableAttack() => enemyTrigger.EnableAttack();

        #endregion public void
    }
}