﻿using Global.ActiveObjects;
using Global.Player;
using System;
using UnityEngine;

namespace Global.Trigger
{
	public class PlayerTriggerChecker : MonoBehaviour
	{
#region Inspector variables

#pragma warning disable
		[SerializeField] private string tagObject; //не используется
		[SerializeField] private bool canAttack;
		[SerializeField] private GameObject player;
#pragma warning restore

#endregion Inspector variables

#region private variables

		private Action action;

#endregion private variables

#region public void

		public void EnableAttack()
		{
			canAttack = true;
		}

		public void DisableAttack()
		{
			canAttack = false;
		}

		public GameObject GetPlayer() => player;

		public void AddEvent(Action action)
		{
			this.action += action;
		}

		public void RestoreEvents()
		{
			action = null;
		}

#endregion public void

#region Unity functions

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (collision.GetComponentInParent<PlayerController>())
			{
				if (canAttack)
				{
					if (player == null || player != collision.gameObject)
					{
						player = collision.gameObject;

						if (gameObject.activeInHierarchy)
						{
							action?.Invoke();
						}
					}
				}
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			//перента может не быть у обьекта - переписывай
			if (collision.GetComponentInParent<PlayerController>())
			{
				if (gameObject.transform.parent.gameObject.activeSelf) //ты шо, ты куда, такого никогда не пиши
					//в будущем ты просто не поймешь что ты тут делал
				{
					gameObject.GetComponentInParent<EnemyController>().DisableAttack();
					action?.Invoke();
				}
			}
		}

#endregion Unity functions
	}
}