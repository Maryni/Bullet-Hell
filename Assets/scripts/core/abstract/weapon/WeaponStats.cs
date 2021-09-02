using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
	[CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/WeaponStats")]
	public class WeaponStats : ScriptableObject
	{
		public WeaponType weaponType;
		public int bulletCount;
		public float cooldownTime;
		public float shootingRate;

		//Оно не будет работать в релизе
		//Бахнешь билд - будет проблема такая же, как было
		//Сериализируй этот класс и сохраняй в плеер префс
		private void OnDisable()
		{
			UnityEditor.EditorUtility.SetDirty(this);
		}
	}
}