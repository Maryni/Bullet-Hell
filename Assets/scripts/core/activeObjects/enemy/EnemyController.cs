using Global.Trigger;
using Global.Weapon;
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