using Global.Bullet;
using Global.Managers;
using Global.Shooting;
using System;
using System.Collections;
using UnityEngine;

namespace Global.Weapon
{
	public class AutomaticGunWeapon : BaseWeapon
	{
#region Unity functions

		private void Start()
		{
			Init();
			bulletCountCurrent = weaponStats.bulletCount;
		}

#endregion Unity functions

#region public void

		//у тебя AutomaticGunWeapon и RocketLaucherWeapon Shoot функции похожи
		//подумай как и перепиши ее

		protected override IEnumerator Shoot(Vector2 mousePos, Transform transformParent, Action callback = null)
		{
			bulletCountCurrent--;
			float zParentRotation = gameObject.transform.parent.transform.rotation.eulerAngles.z;
			var bullet = Services.GetManager<PoolManager>().BulletPool.GetObject(WeaponType);

			//ты можешь все эти штуки вызывать при инициализации, перепиши

			// bullet.transform.position = transformParent.position;
			// bullet.gameObject.SetActive(true);
			// bullet.ActivateBullet();
			// bullet.Rotate(zParentRotation);
			// bullet.Move();
			// bullet.Rotate(correctAngleForSprite + zParentRotation);
			yield return new WaitForSeconds(weaponStats.shootingRate);
			if (bulletCountCurrent == 0)
			{
				yield return Reload();
			}

			StartCoroutine(base.Shoot(mousePos, transformParent, callback));
			callback?.Invoke();
		}

#endregion public void
	}
}