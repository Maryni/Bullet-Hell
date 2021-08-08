using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Weapon
{
    public class RocketLaucherWeapon : BaseWeapon
    {
        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public void

        public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
        {
            //колбек не используеться
            //значит он здесь не нужен
            //то что ты не используешь - должно удаляться
            //если оно мешает удалению, значит неправильно построил архитектуру или же не правильно ее используешь
            var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
            bullet.transform.parent = transformParent;
            bullet.gameObject.SetActive(true);
            bullet.Move(transformCanon);
            yield return new WaitForSeconds(.1f); //магическое число
        }

        #endregion public void
    }
}