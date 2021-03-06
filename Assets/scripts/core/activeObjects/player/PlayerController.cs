using Global.Controllers;
using Global.Managers.Datas;
using UnityEngine;
using System.Linq;
using Global.Game.Component;
using Tools;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Global.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Player player;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private ShootController shootController;
        [SerializeField] private GameObject losePanel;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public ShootController ShootController => shootController;
        public HUDController HUDController => hUDController;

        #endregion properties

        #region private variables

        private HUDController hUDController;

        private SmoothPause smoothPause;
        private bool canDamaged = true;
        private Coroutine immortalityCoroutine;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            SetPlayerStatsFromData();
            hUDController = FindObjectOfType<HUDController>();
            shootController.SetWeapon(Services.GetManager<DataManager>().DynamicData.StartPlayerWeapon);
            smoothPause = FindObjectOfType<SmoothPause>();

            player.RestoreHPToMaxHP();
            hUDController.SetHPMaximum(player.HPMaximum);
        }

        #endregion Unity functions

        #region public void

        public void RefreshPlayerStatsFromData()
        {
            SetPlayerStatsFromData();
        }

        public WeaponType GetWeaponTypeByPlayer()
        {
            return shootController.CurrentWeapon.WeaponType;
        }

        public bool IsPlayerIsDead()
        {
            if (player.hpValue <= 0)
            {
                player.hpValue = 0;
                return true;
            }
            return false;
        }

        public void DamagePlayer(float damage)
        {
            if (canDamaged)
            {
                player.hpValue -= (CalculateDamage(damage));
            }
            if (IsPlayerIsDead())
            {
                smoothPause.StartPauseWhenDead();
            }
            hUDController.GlowingByType(TypeGlowing.HpCurrent, player.hpValue);
        }

        public void EnableImmortalityOnTime(float time)
        {
            if (immortalityCoroutine == null)
            {
                immortalityCoroutine = StartCoroutine(Immortality(time));
            }
        }

        #endregion public void

        #region private void

        private IEnumerator Immortality(float time)
        {
            print("defence");
            canDamaged = false;
            yield return new WaitForSeconds(time);
            canDamaged = true;
            immortalityCoroutine = null;
        }

        private float CalculateDamage(float damage)
        {
            var hpDecrease = damage - player.Defence;
            if (hpDecrease < 0)
            {
                hpDecrease = 0;
            }
            return hpDecrease;
        }

        private void SetPlayerStatsFromData()
        {
            var data = Services.GetManager<DataManager>().DynamicData.PlayerData;
            player.SetPlayerStatFromData(data.hp, data.speed, data.defence);
        }

        #endregion private void
    }
}