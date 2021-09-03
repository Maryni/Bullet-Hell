using Global.Managers.Datas;
using Global.Player;
using System.Collections;
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

        #endregion private variables

        #region Unity functions

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

        public void AddScore(int value)
        {
            scoreValue += value;
            GlowingByType(TypeGlowing.Score, scoreValue);
        }

        public void GlowingByType(TypeGlowing typeGlowing, float valueTaken)
        {
            float range = maxAlpha - minAlpha;
            float step = range / timeGlowing;
            if (typeGlowing == TypeGlowing.Score)
            {
                StopCoroutine(Glow(scoreText, step));
                StopCoroutine(Glow(scoreTextValue, step));
                StopAllCoroutines();
                StartCoroutine(Glow(scoreText, step));
                StartCoroutine(Glow(scoreTextValue, step));
                scoreTextValue.text = valueTaken.ToString();
            }
            if (typeGlowing == TypeGlowing.HpCurrent)
            {
                StopCoroutine(Glow(hpText, step));
                StopCoroutine(Glow(hpCurrentTextValue, step));
                StopAllCoroutines();
                StartCoroutine(Glow(hpText, step));
                StartCoroutine(Glow(hpCurrentTextValue, step));
                hpCurrentTextValue.text = valueTaken.ToString();
            }
            if (typeGlowing == TypeGlowing.HpMaximum)
            {
                StopCoroutine(Glow(hpText, step));
                StopCoroutine(Glow(hpMaximumTextValue, step));
                StopAllCoroutines();
                StartCoroutine(Glow(hpText, step));
                StartCoroutine(Glow(hpMaximumTextValue, step));
                hpMaximumTextValue.text = valueTaken.ToString();
            }
            if (typeGlowing == TypeGlowing.BulletsCurrent)
            {
                //StopCoroutine(Glow(bulletstText, step));
                //StopCoroutine(Glow(bulletsCurrentTextValue, step));
                StopAllCoroutines();
                StartCoroutine(Glow(bulletstText, step));
                StartCoroutine(Glow(bulletsCurrentTextValue, step));
                bulletsCurrentTextValue.text = valueTaken.ToString();
            }
            if (typeGlowing == TypeGlowing.BulletsMaximum)
            {
                StopCoroutine(Glow(bulletstText, step));
                StopCoroutine(Glow(bulletsMaximumTextValue, step));
                StopAllCoroutines();
                StartCoroutine(Glow(bulletstText, step));
                StartCoroutine(Glow(bulletsMaximumTextValue, step));
                bulletsMaximumTextValue.text = valueTaken.ToString();
            }
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
        }

        #endregion private void
    }
}