﻿using UnityEngine;
using Global.Shooting;
using Global.Managers.Datas;
using Global.Player;

namespace Global.Controllers
{
    public class ShootController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable

        [SerializeField] private BaseWeapon baseWeapon;
        [SerializeField] private Transform cannonTransform;
        [SerializeField] private Transform bulletPool;
        [SerializeField] private Vector2 mousePos;
        [SerializeField] private PlayerController playerController;

#pragma warning restore

        #endregion Inspector variables

        #region properties

        public BaseWeapon CurrentWeapon => baseWeapon;

        #endregion properties

        #region Unity function

        private void OnValidate()
        {
            if (playerController == null)
            {
                playerController = GetComponent<PlayerController>();
            }
        }

        private void Update()
        {
            cannonTransform.up = Rotation(cannonTransform);
            GetReadyShootByWeapon();
        }

        #endregion Unity function

        #region public void

        public void SetWeapon(BaseWeapon baseWeapon)
        {
            this.baseWeapon = baseWeapon;
        }

        #endregion public void

        #region private void

        private void GetReadyShootByWeapon()
        {
            if (!playerController.IsPlayerIsDead())
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    baseWeapon.Shoot(mousePos, bulletPool);
                }
            }
        }

        private Vector2 Rotation(Transform transformObject)
        {
            mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mousePos - (Vector2)transformObject.position).normalized;
        }

        #endregion private void
    }
}