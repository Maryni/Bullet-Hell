using Global.Managers.Datas;
using Global.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Controllers
{
	public class HUDController : MonoBehaviour
	{
#region Inspector variables

#pragma warning disable
		[SerializeField] private float timeGlowing;
		[SerializeField] private int minAlpha;
		[SerializeField] private int maxAlpha;
		[SerializeField] private Text scoreText;
		[SerializeField] private Text scoreTextValue;
		[SerializeField] private Text hpText;
		[SerializeField] private Text hpMaximumTextValue;
		[SerializeField] private Text hpCurrentTextValue;
		[SerializeField] private Text bulletstText; //нейминг
		[SerializeField] private Text bulletsCurrentTextValue;
		[SerializeField] private Text bulletsMaximumTextValue;
#pragma warning restore

#endregion Inspector variables

#region private variables

		private PlayerController playerController;
		private int scoreValue;

#endregion private variables

#region Unity functions

		private void Start()
		{
			if (playerController == null) //тебе здесь проверка на нул не нужна,
				//он у тебя и так нулл, и ты его ищешь один раз при старте
			{
				playerController = FindObjectOfType<PlayerController>();
			}

			SetValues();
		}

#endregion Unity functions

#region public void

		public void AddScore(int value)
		{
			scoreValue += value;
			SetValueToScore();
			StopAllCoroutines();
			StartCoroutine(Glowing(scoreText, scoreTextValue));
		}

#endregion public void

#region private void

		private IEnumerator Glowing(params Text[] texts)
		{
			float value = minAlpha;
			float range = maxAlpha - minAlpha;
			float step = range / timeGlowing;
			value = ((Color32)texts[0].color).a;
			while (value < maxAlpha)
			{
				value += (step * Time.deltaTime);
				for (int i = 0; i < texts.Length; i++)
				{
					Color32 temp = texts[i].color;
					temp.a = (byte)value;
					texts[i].color = temp;
				}

				yield return null;
			}

			while (value > minAlpha)
			{
				value -= (step * Time.deltaTime);
				for (int i = 0; i < texts.Length; i++)
				{
					Color32 temp = texts[i].color;
					temp.a = (byte)value;
					texts[i].color = temp;
				}

				yield return null;
			}

			yield break;
		}

		private void SetValues()
		{
			GetValueFromScore();
			SetValueHPFromPlayer();
			SetValueBulletsCurrentFromPlayer();
			SetValueBulletsMaximumFromPlayer();
			SubscribeForChanges();
		}

		private void SubscribeForChanges()
		{
			//Миша, давай по новой
			//Это не совсем хорошая реализация
			//Класс игрока или вообще любой другой класс не должны знать, что в них могут добавить что-то
			//В данном случае почитай за event
			//И делай это так, чтобы у тебя была здесь одна функция, которая будет делать что-то, что ее вызовут,
			//а не плоди кучу событий и добавляй их отдельно

			// playerController.AddEvent(() => SetValueHPFromPlayer());
			// playerController.AddEvent(() => StopAllCoroutines());
			// playerController.AddEvent(() => StartCoroutine(Glowing(hpText, hpCurrentTextValue, hpMaximumTextValue)));
			// playerController.ShootController.AddEventToCurrentBulletsChange(() => SetValueBulletsCurrentFromPlayer());
			// playerController.ShootController.AddEventToCurrentBulletsChange(() => StopAllCoroutines());
			// playerController.ShootController.AddEventToCurrentBulletsChange(() =>
			// 	StartCoroutine(Glowing(bulletstText, bulletsCurrentTextValue)));
			// playerController.ShootController.AddEventToMaximumBulletsChange(() => SetValueBulletsMaximumFromPlayer());
			// playerController.ShootController.AddEventToMaximumBulletsChange(() => StopAllCoroutines());
			// playerController.ShootController.AddEventToCurrentBulletsChange(() =>
			// 	StartCoroutine(Glowing(bulletstText, bulletsMaximumTextValue)));
		}

		private void SetValueHPFromPlayer()
		{
			hpCurrentTextValue.text = playerController.GetHpPlayerCurrent().ToString();
			hpMaximumTextValue.text = playerController.GetHpPlayerMaximum().ToString();
		}

		private void SetValueBulletsCurrentFromPlayer()
		{
			bulletsCurrentTextValue.text = playerController.GetCountCurrentBulletsByCurrentWeapon().ToString();
		}

		private void SetValueBulletsMaximumFromPlayer()
		{
			bulletsMaximumTextValue.text = playerController.GetCountMaxBulletsByCurrentWeapon().ToString();
		}

		private void GetValueFromScore()
		{
			scoreValue = int.Parse(scoreTextValue.text);
		}

		private void SetValueToScore()
		{
			scoreTextValue.text = scoreValue.ToString();
		}

#endregion private void
	}
}