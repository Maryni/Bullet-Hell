using Global.Player;
using Global.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Weapon
{
    public class MeleeWeapon : BaseWeapon
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private PlayerTriggerChecker playerTriggerChecker;
        [SerializeField] private GameObject player;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private bool canAttack;
        private int bulletCountCurrent;

        #endregion private variables

        #region Unity function

        private void Start()
        {
            Init();
        }

        private void OnValidate()
        {
            playerTriggerChecker = GetComponent<PlayerTriggerChecker>();
        }

        #endregion Unity function

        #region public void

        public override IEnumerator Reload()
        {
            canAttack = true;
            if (bulletCountCurrent <= 0)
            {
                canAttack = false;
                yield return new WaitForSeconds(weaponStats.cooldownTime);
                bulletCountCurrent = weaponStats.bulletCount;
                canAttack = true;
                yield return canAttack;
            }
            yield return canAttack;
        }

        public void Attack()
        {
            if (canAttack)
            {
                StartCoroutine(Reload());
                player = playerTriggerChecker.GetPlayer();
                if (player != null)
                {
                    player.GetComponent<Global.Player.Player>().StartCoroutine("ObjectTriggered");
                }
            }
        }

        #endregion public void
    }
}