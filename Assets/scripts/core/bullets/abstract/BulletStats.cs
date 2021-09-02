using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
	[CreateAssetMenu(fileName = "BulletStats", menuName = "Bullet/BulletStats")]
	public class BulletStats : ScriptableObject
	{
		public BulletType bulletType;
		public int damage;
		public float speed;

		//Оно не будет работать в релизе
		//Бахнешь билд - будет проблема такая же, как было
		//Сериализируй этот класс и сохраняй в плеер префс
		private void OnDisable()
		{
			UnityEditor.EditorUtility.SetDirty(this);
		}
	}
}