using Global.Controllers;
using Global.Managers.Datas;
using Global.Shooting;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Global.Game.Component;
using Tools;

namespace Global.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Player player;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private ShootController shootController;
        [SerializeField] private List<BaseWeapon> listWeapons;

#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private SmoothPause smoothPause;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            SetWeaponInPlayer(Services.GetManager<DataManager>().DynamicData.StartPlayerWeapon);
            smoothPause = FindObjectOfType<SmoothPause>();
            player.RestoreHPToMaxHP();
        }

        #endregion Unity functions

        #region public void

        public WeaponType GetWeaponTypeByPlayer()
        {
            return shootController.CurrentWeapon.WeaponType;
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
                smoothPause.StartPause();
                SceneLoader.LoadScene(1);
                var controller = FindObjectOfType<GameController>();
                controller.DisableSpawnedItems();
                controller.DisableSpawningEverything();
                smoothPause.ResetTimeScale();
            }
        }

        public void DamagePlayer(float damage)
        {
            player.DecreaseHp(CalculateDamage(damage));
            if (IsPlayerIsDead())
            {
                smoothPause.StartPause();
                SceneLoader.LoadScene(1);
                var controller = FindObjectOfType<GameController>();
                controller.DisableSpawnedItems();
                controller.DisableSpawningEverything();
                smoothPause.ResetTimeScale();
            }
        }

        public void SetPlayerStatsFromData()
        {
            var data = Services.GetManager<DataManager>().DynamicData.PlayerData;
            player.SetPlayerStatFromData(data.hp, data.speed, data.defence);
        }

        public void SetWeaponInPlayer(WeaponType weaponType)
        {
            var weaponScript = listWeapons.FirstOrDefault(x => x.WeaponType == weaponType);
            shootController.SetWeapon(weaponScript);
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