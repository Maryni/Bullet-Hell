using Global.Controllers;
using Global.Managers.Datas;
using Global.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        #region Unity functions

        private void Start()
        {
            SetWeaponInPlayer(Services.GetManager<DataManager>().DynamicData.StartPlayerWeapon);
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
        }

        #endregion public void

        #region private void

        private void SetWeaponInPlayer(WeaponType weaponType)
        {
            var weaponScript = listWeapons.FirstOrDefault(x => x.WeaponType == weaponType);
            shootController.SetWeapon(weaponScript);
        }

        #endregion private void
    }
}