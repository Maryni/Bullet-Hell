using Global.Controllers;
using Global.Managers;
using Global.Managers.Datas;
using Global.Player;
using Global.Shooting.BulletSpace;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
    public class WeaponChanger : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Sprite sprite;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private GameObject player;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public WeaponType WeaponType => weaponType;

        #endregion properties

        #region Unity functions

        private void Start()
        {
            if (sprite != null)
            {
                spriteRenderer.sprite = sprite;
            }
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>().gameObject;
            }
        }

        //get collision with player and say GameController change playerWeapon by my weapon
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == TriggerType.Player.ToString() && collision.isTrigger)
            {
                SetPlayerWeaponByWeapon(weaponType);
                gameObject.SetActive(false);
            }
        }

        #endregion Unity functions

        #region public void

        public void SetWeaponRandom()
        {
            weaponType = Services.GetManager<PoolManager>().WeaponPool.GetWeaponTypeExclusivePlayerWeapon();
        }

        public void SetPlayerWeaponByWeapon(WeaponType weaponType)
        {
            player.GetComponentInChildren<ShootController>().SetWeapon(weaponType);
        }

        public IEnumerator DisableObjectByTime(int time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }

        #endregion public void
    }
}