using Global.Managers.Datas;
using Global.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Controllers
{
    public enum TypeGlowing
    {
        Score,
        HpCurrent,
        HpMaximum,
        BulletsCurrent,
        BulletsMaximum
    }

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
        [SerializeField] private Text bulletstText;
        [SerializeField] private Text bulletsCurrentTextValue;
        [SerializeField] private Text bulletsMaximumTextValue;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private int scoreValue;
        private PlayerController playerController;
        private Dictionary<TypeGlowing, Action<float, float>> types = new Dictionary<TypeGlowing, Action<float, float>>();
        private float range;
        private float step;

        #endregion private variables

        #region Unity functions

        private void Awake()
        {
            AddActionsToDictionary();
        }

        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        #endregion Unity functions

        #region public void

        public void SetHPMaximum(float value)
        {
            hpMaximumTextValue.text = value.ToString();
        }

        public void GlowingByType(TypeGlowing typeGlowing, float valueTaken)
        {
            range = maxAlpha - minAlpha;
            step = range / timeGlowing;
            types[typeGlowing]?.Invoke(valueTaken, step);
        }

        #endregion public void

        #region private void

        private IEnumerator Glow(Text text, float step)
        {
            float value = maxAlpha;
            Color32 temp = text.color;
            temp.a = (byte)value;
            text.color = temp;
            while (value > minAlpha)
            {
                value -= (step * Time.deltaTime);

                temp = text.color;
                temp.a = (byte)value;
                text.color = temp;
                yield return null;
            }
            yield break;
        }

        private void AddActionsToDictionary()
        {
            types.Add(TypeGlowing.Score, (float valueTaken, float step) => GlowScore(valueTaken, step));
            types.Add(TypeGlowing.HpCurrent, (float valueTaken, float step) => GlowHpCurrent(valueTaken, step));
            types.Add(TypeGlowing.HpMaximum, (float valueTaken, float step) => GlowHpMaximum(valueTaken, step));
            types.Add(TypeGlowing.BulletsCurrent, (float valueTaken, float step) => GlowBulletsCurrent(valueTaken, step));
            types.Add(TypeGlowing.BulletsMaximum, (float valueTaken, float step) => GlowBulletsMaximum(valueTaken, step));
        }

        private void GlowScore(float valueTaken, float step)
        {
            scoreValue += (int)valueTaken;
            StopCoroutine(Glow(scoreText, step));
            StopCoroutine(Glow(scoreTextValue, step));
            StartCoroutine(Glow(scoreText, step));
            StartCoroutine(Glow(scoreTextValue, step));
            scoreTextValue.text = scoreValue.ToString();
        }

        private void GlowHpCurrent(float valueTaken, float step)
        {
            StopCoroutine(Glow(hpText, step));
            StopCoroutine(Glow(hpCurrentTextValue, step));
            StartCoroutine(Glow(hpText, step));
            StartCoroutine(Glow(hpCurrentTextValue, step));
            hpCurrentTextValue.text = valueTaken.ToString();
        }

        private void GlowHpMaximum(float valueTaken, float step)
        {
            StopCoroutine(Glow(hpText, step));
            StopCoroutine(Glow(hpMaximumTextValue, step));
            StartCoroutine(Glow(hpText, step));
            StartCoroutine(Glow(hpMaximumTextValue, step));
            hpMaximumTextValue.text = valueTaken.ToString();
        }

        private void GlowBulletsCurrent(float valueTaken, float step)
        {
            StopAllCoroutines();
            StartCoroutine(Glow(bulletstText, step));
            StartCoroutine(Glow(bulletsCurrentTextValue, step));
            bulletsCurrentTextValue.text = valueTaken.ToString();
        }

        private void GlowBulletsMaximum(float valueTaken, float step)
        {
            StopCoroutine(Glow(bulletstText, step));
            StopCoroutine(Glow(bulletsMaximumTextValue, step));
            StartCoroutine(Glow(bulletstText, step));
            StartCoroutine(Glow(bulletsMaximumTextValue, step));
            bulletsMaximumTextValue.text = valueTaken.ToString();
        }

        #endregion private void
    }
}