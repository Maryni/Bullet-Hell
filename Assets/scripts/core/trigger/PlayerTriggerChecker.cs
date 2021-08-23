﻿using Global.ActiveObjects;
using Global.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//много не нужных неймспейсов

//нет неймспейса
public class PlayerTriggerChecker : MonoBehaviour
{
#region Inspector variables

#pragma warning disable
	[SerializeField] private string tagObject;
	[SerializeField] private bool canAttack;
	[SerializeField] private GameObject player;
	private Action action; // форматирование не правильное
#pragma warning restore

#endregion Inspector variables

#region public void

	//////////////////////////////////
	// СКОБКИ
	// Или через свойсвта
	public void EnableAttack() => canAttack = true;

	public void DisableAttack() => canAttack = false;

	public GameObject GetPlayer() => player;
	//////////////////////////////////

	public void AddEvent(Action action)
	{
		this.action += action;
	}

#endregion public void

#region Unity functions

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == tagObject)
		{
			if (tagObject == "Player" && canAttack)
			{
				if (player == null || player != collision.gameObject)
				{
					player = collision.gameObject;
					Debug.Log($"[ {collision.name} ] are triggered by me [ {gameObject.transform.parent.name} ]");
					action?.Invoke();
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == tagObject)
		{
			if (tagObject == "Player")
			{
				gameObject.GetComponentInParent<EnemyController>().DisableAttack();
				action?.Invoke();
				Debug.Log("#1 ");
			}
		}
	}

#endregion Unity functions
}