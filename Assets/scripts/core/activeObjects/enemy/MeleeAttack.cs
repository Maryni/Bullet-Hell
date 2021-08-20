using Global.Managers.Datas;
using Global.Player;
using Global.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Weapon
{
    public class MeleeAttack : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private PlayerTriggerChecker playerTriggerChecker;
        [SerializeField] private GameObject player;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        [SerializeField] private bool canAttack;

        #endregion private variables

        #region Unity function

        private void OnValidate()
        {
            playerTriggerChecker = GetComponent<PlayerTriggerChecker>();
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
        }

        public IEnumerator Attack(EnemyType enemyType)
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
                yield return Attack(enemyType);
            }
        }

        #endregion public void
    }
}