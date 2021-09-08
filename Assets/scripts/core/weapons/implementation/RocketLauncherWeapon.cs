using Global.Bullet;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
    public class RocketLauncherWeapon : BaseWeapon
    {
        #region Unity functions

        private void Start()
        {
            Init();
            bulletCountCurrent = weaponStats.bulletCount;
        }

        #endregion Unity functions

        #region public void

        protected override IEnumerator Shooting(Vector2 mousePos, Transform transformParent, bool enableRotation)
        {
            StartCoroutine(base.Shooting(mousePos, transformParent, false));
            yield return null;
        }

        #endregion public void
    }
}