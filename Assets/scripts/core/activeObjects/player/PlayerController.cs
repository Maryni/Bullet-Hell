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
        [SerializeField] private SmoothPause smoothPause;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            SetWeaponInPlayer(Services.GetManager<DataManager>().DynamicData.StartPlayerWeapon);
            smoothPause = FindObjectOfType<SmoothPause>();
        }

        #endregion Unity functions

        #region public void

        public bool IsPlayerIsDead()
        {
            if (player.IsDead())
            {
                return true;
            }
            return false;
        }

        public void DamagePlayer(int damage)
        {
            player.GetDamage(damage);
            if (IsPlayerIsDead())
            {
                smoothPause.StartPause();
                SceneLoader.LoadScene(1);
                var controller = FindObjectOfType<GameController>();
                controller.DisableSpawnedItems();
                controller.DisableSpawningEverything();
            }
        }

        public void DamagePlayer(float damage)
        {
            player.GetDamage(damage);
            if (IsPlayerIsDead())
            {
                smoothPause.StartPause();
                SceneLoader.LoadScene(1);
                var controller = FindObjectOfType<GameController>();
                controller.DisableSpawnedItems();
                controller.DisableSpawningEverything();
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
    }
}