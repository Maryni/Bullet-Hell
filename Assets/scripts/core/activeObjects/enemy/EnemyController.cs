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
            if (enemyTrigger == null)
            {
                enemyTrigger = GetComponentInChildren<PlayerTriggerChecker>();
            }
            if (meleeEnemy == null)
            {
                meleeEnemy = GetComponent<MeleeEnemy>();
            }
            if (meleeAttack == null)
            {
                meleeAttack = GetComponentInChildren<MeleeAttack>();
            }
        }

        #endregion Unity functions

        #region public void

        public void SetPlayerTransform()
        {
            meleeEnemy.SetTransformPlayer();
            meleeEnemy.ResetEnemyHP();
        }

        public void DamageEnemy(int damage)
        {
            meleeEnemy.ObjectTriggered(damage);
        }

        public void ActivateEnemy()
        {
            SetPlayerTransform();
            meleeEnemy.Movement();
            EnableAttack();
        }

        public void EnableAttack()
        {
            enemyTrigger.EnableAttack();
            enemyTrigger.AddEvent(() => meleeAttack.EnableAttack());
            enemyTrigger.AddEvent(() => StartCoroutine(meleeAttack.Attack(meleeEnemy.EnemyStats.enemyType)));
        }

        public void DisableAttack()
        {
            enemyTrigger.DisableAttack();
            enemyTrigger.AddEvent(() => meleeAttack.DisableAttack());
        }

        #endregion public void
    }
}