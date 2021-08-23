﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Interfaces;
using Global.Managers.Datas;

namespace Global.Player
{
	public class Player : MonoBehaviour
	{
#region private variables

#pragma warning disable

		[SerializeField] private int hpMaximum;
		[SerializeField] private int speed;
		[SerializeField] private int defence;
		[SerializeField] private float hpValue;

#pragma warning restore

#endregion private variables

#region properties

		//нейминг
		public int HP => hpMaximum;
		public float Speed => speed;
		public float HPValue => hpValue;

#endregion properties

#region Unity functions

		private void Start()
		{
			RestoreHPToMaxHP();
		}

#endregion Unity functions

#region public void

		//Это все должно быть не в игроке
		//Игрок служит для хранения данных, связаных с игроком, которые будут как-то меняться
		//Реализация должна быть в контроллере

		// public void SetPlayerStatFromData(int hp, int speed, int defence)
		// {
		//     hpMaximum = hp;
		//     this.defence = defence;
		//     this.speed = speed;
		// }
		//
		// public bool IsDead()
		// {
		//     if (hpValue < 0)
		//     {
		//         return true;
		//     }
		//     return false;
		// }
		//
		// public void GetDamage(int damage)
		// {
		//     Debug.Log($"Im [ {name} ] triggered, I take {damage} damage");
		//     CalculateDamage(damage);
		//     if (IsDead())
		//     {
		//         Debug.Log("Player die");
		//     }
		// }
		//
		// public void GetDamage(float damage)
		// {
		//     Debug.Log($"Im [ {name} ] triggered, I take {damage} damage");
		//     CalculateDamage(damage);
		//     if (IsDead())
		//     {
		//         Debug.Log("Player die");
		//     }
		// }
		//
		// #endregion public void
		//
		// #region private void
		//
		// private void RestoreHPToMaxHP()
		// {
		//     hpValue = hpMaximum;
		// }
		//
		// private void CalculateDamage(int damage)
		// {
		//     var hpDecrease = damage - defence;
		//     if (hpDecrease < 0)
		//     {
		//         hpDecrease = 0;
		//     }
		//     hpValue -= hpDecrease;
		// }
		//
		// private void CalculateDamage(float damage)
		// {
		//     var hpDecrease = damage - defence;
		//     if (hpDecrease < 0)
		//     {
		//         hpDecrease = 0;
		//     }
		//     hpValue -= hpDecrease;
		// }

#endregion private void
	}
}