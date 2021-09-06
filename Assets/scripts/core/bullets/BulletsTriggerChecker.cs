using Global.ActiveObjects;
using Global.Player;
using UnityEngine;
using Global.Bullet;
using System.Collections.Generic;
using System.Collections;

namespace Global.Shooting.BulletSpace
{
	public enum TriggerType
	{
		Player,
		Enemy,
		Bullet
	}

	public class BulletsTriggerChecker : MonoBehaviour
	{
#region Inspector variables

#pragma warning disable
		[SerializeField] private TriggerType triggerType;
		[SerializeField] private bool useMeOrTriggerObject; //true - me, false - triggerObject
		[SerializeField] private bool dealDamage;
		[SerializeField] private bool mustDisable;
		[SerializeField] private BaseBullet baseBullet;
#pragma warning restore

#endregion Inspector variables

#region private variables

		private List<GameObject> listTouchingObjects = new List<GameObject>();

#endregion private variables

#region Unity function

		private void OnValidate()
		{
			if (baseBullet == null)
			{
				baseBullet = GetComponent<BaseBullet>();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<EnemyController>())
			{
				if (!listTouchingObjects.Contains(collision.gameObject))
				{
					listTouchingObjects.Add(collision.gameObject);
				}

				//По сути, у тебя тут должен быть просто вызов реализации пули, не проверяя что-то конкретное
				//а уже внутри реализации пуль контроллер решит что нужно будет с этим сделать
				//когда у тебя есть такие проверки, значит у тебя архитектура построена не правильно
				if (baseBullet.BulletStats.bulletType == Managers.Datas.BulletType.RocketLaucherBullet)
				{
					((RocketLaucherBullet)baseBullet).ExplosiveRadiusUp();
					StartCoroutine(LateCall());
				}
				else
				{
					collision.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
					gameObject.SetActive(false);
				}
			}

			if (collision.GetComponent<BulletsTriggerChecker>() &&
				collision.GetComponent<BulletsTriggerChecker>().triggerType == TriggerType.Bullet)
			{
				gameObject.SetActive(false);
			}

#endregion Unity function
		}

		//карутина плохая, ее нужно переписать
		private IEnumerator
			LateCall() //что за нейминг? что такое лейт кол? за что он отвечает? Название не дает понимая, что тут происходит
		{
			yield return new WaitForFixedUpdate(); //что?
			if (baseBullet.BulletStats.bulletType == Managers.Datas.BulletType.RocketLaucherBullet &&
				listTouchingObjects.Count > 0)
			{
				yield return new WaitForFixedUpdate(); //что?
				yield return new WaitForEndOfFrame(); //что?
				//зачем ты тут ждешь сначала обновление физики и потом конец кадра?
				foreach (GameObject gameObjectItem in listTouchingObjects)
				{
					if (gameObjectItem.activeInHierarchy)
					{
						gameObjectItem.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
					}
				}

				listTouchingObjects.Clear();
				gameObject.GetComponent<RocketLaucherBullet>().ExplosiveRadiusDown();
			}

			gameObject.SetActive(false);
			yield break; //оно не используеться, удОли
		}
	}
}