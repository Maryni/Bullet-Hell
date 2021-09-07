using Global.Trigger;
using Global.Weapon;
using System;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class EnemyController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private BaseEnemy meleeEnemy;
        [SerializeField] private PlayerTriggerChecker enemyTrigger;
        [SerializeField] private MeleeAttack meleeAttack;
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private ScoreItem scoreItem;

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
            if (scoreItem == null)
            {
                scoreItem = GetComponent<ScoreItem>();
            }
        }

        #endregion Unity functions

        #region public void

        public void AddEventToEnemy(Action action)
        {
            if (meleeEnemy.ActionOnDie != action)
            {
                meleeEnemy.AddEventOnDie(action);
            }
        }

        public void SetPlayerTransform()
        {
            meleeEnemy.SetTransformPlayer();
        }

        public void DamageEnemy(int damage)
        {
            meleeEnemy.GetDamage(damage);
        }

        public void ActivateEnemy()
        {
            SetPlayerTransform();
            meleeEnemy.ResetEnemyHP();
            enemyMovement.Move(meleeEnemy.TransformPlayer, meleeEnemy.Rig2D, meleeEnemy.EnemyStats.speed);
            EnableAttack();
            scoreItem.FindAndSetHUDController();
        }

        public void EnableAttack()
        {
            if (meleeEnemy.gameObject.activeInHierarchy)
            {
                enemyTrigger.EnableAttack();
                enemyTrigger.AddEvent(() => meleeAttack.EnableAttack());
                enemyTrigger.AddEvent(() => meleeAttack.Attack(meleeEnemy.EnemyStats.enemyType));
            }
        }

        public void DisableAttack()
        {
            StopAllCoroutines();
            enemyTrigger.RestoreEvents();
            enemyTrigger.AddEvent(() => StopAllCoroutines());
            enemyTrigger.DisableAttack();
            enemyTrigger.AddEvent(() => meleeAttack.DisableAttack());
        }

        #endregion public void
    }
}