using Global;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGunWeapon : BaseWeapon
{
    #region Unity functions

    private void Start()
    {
        Init();
    }

    #endregion Unity functions

    #region public void

    public override IEnumerator Shoot(Vector2 mousePos, Transform transformCanon, Transform transformParent, Action callback = null)
        //mousePos не используется
        //колбек не используется
        //значит он здесь не нужен
        //то что ты не используешь - должно удаляться
        //если оно мешает удалению, значит неправильно построил архитектуру или же не правильно ее используешь
    {
        var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);
        bullet.transform.position = transformParent.position;
        bullet.gameObject.SetActive(true);
        bullet.Move(transformCanon);
        yield return new WaitForSeconds(.1f); //магическое число
    }

    #endregion public void
}