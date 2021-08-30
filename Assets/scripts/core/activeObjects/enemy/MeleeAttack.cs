using Global.Managers.Datas;
using Global.Player;
using Global.Trigger;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
    public class MeleeAttack : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private PlayerTriggerChecker playerTriggerChecker;
        [SerializeField] private GameObject player;
        [SerializeField] private bool canAttack;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private Coroutine coroutine;

        #endregion private variables

        #region Unity function

        private void OnValidate()
        {
            if (playerTriggerChecker == null)
            {
                playerTriggerChecker = GetComponent<PlayerTriggerChecker>();
            }
        }

        #endregion Unity function

        #region public void

        public void EnableAttack()
        {
            canAttack = true;
        }

        public void DisableAttack()
        {
            canAttack = false;
            StopCoroutineAttack();
        }

        public void Attack(EnemyType enemyType)
        {
            if (gameObject.transform.parent.gameObject.activeInHierarchy && coroutine == null)
            {
                coroutine = StartCoroutine(Attacking(enemyType));
            }
        }

        #endregion public void

        #region private void

        private IEnumerator Attacking(EnemyType enemyType)
        {
            if (canAttack)
            {
                var stats = Services.GetManager<DataManager>().StaticData.GetEnemyStatsByType(enemyType);
                player = playerTriggerChecker.GetPlayer();
                if (player != null)
                {
                    player.GetComponent<PlayerController>().DamagePlayer(stats.damage);
                    player = null;
                }
                yield return new WaitForSeconds(stats.attackRate);
                yield return Attacking(enemyType);
            }
        }

        private void StopCoroutineAttack()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        #endregion private void
    }
}