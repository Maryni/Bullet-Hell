using Global.Controllers;
using Global.Managers.Datas;
using UnityEngine;
using System.Linq;
using Global.Game.Component;
using Tools;
using System;

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

        #endregion properties

        #region private variables

        private SmoothPause smoothPause;

        
        private Action action;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            shootController.SetWeapon(Services.GetManager<DataManager>().DynamicData.StartPlayerWeapon);
            smoothPause = FindObjectOfType<SmoothPause>();
            player.RestoreHPToMaxHP();
        }

        #endregion Unity functions

        #region public void

        public void AddEvent(Action action)
        {
            this.action += action;
        }

        public WeaponType GetWeaponTypeByPlayer()
        {
            return shootController.CurrentWeapon.WeaponType;
        }

        public int GetCountMaxBulletsByCurrentWeapon()
        {
            return shootController.CurrentWeapon.WeaponStats.bulletCount;
        }

        public int GetCountCurrentBulletsByCurrentWeapon()
        {
            return shootController.CurrentWeapon.BulletCountCurrent;
        }

        public float GetHpPlayerCurrent()
        {
            return player.HPValue;
        }

        public int GetHpPlayerMaximum()
        {
            return player.HPMaximum;
        }

        public bool IsPlayerIsDead()
        {
            if (player.HPValue <= 0)
            {
                return true;
            }
            return false;
        }

        public void DamagePlayer(int damage)
        {
            player.DecreaseHp(CalculateDamage(damage));
            if (IsPlayerIsDead())
            {
                smoothPause.StartPauseWhenDead();
                var controller = FindObjectOfType<GameController>();
                controller.DisableSpawningEverything();
                controller.DisableSpawnedItems();
            }
            action?.Invoke();
        }

        public void DamagePlayer(float damage)
        {
            player.DecreaseHp(CalculateDamage(damage));
            if (IsPlayerIsDead())
            {
                smoothPause.StartPauseWhenDead();
                var controller = FindObjectOfType<GameController>();
                controller.DisableSpawningEverything();
                controller.DisableSpawnedItems();
            }
            action?.Invoke();
        }

        public void SetPlayerStatsFromData()
        {
            var data = Services.GetManager<DataManager>().DynamicData.PlayerData;
            player.SetPlayerStatFromData(data.hp, data.speed, data.defence);
        }

        #endregion public void

        #region private void

        private int CalculateDamage(int damage)
        {
            var hpDecrease = damage - player.Defence;
            if (hpDecrease < 0)
            {
                hpDecrease = 0;
            }
            return hpDecrease;
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

        #endregion private void
    }
}