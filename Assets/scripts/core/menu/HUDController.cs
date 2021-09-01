using Global.Managers.Datas;
using Global.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Controllers
{
    public class HUDController : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Text scoreText;
        [SerializeField] private Text hpMaximumText;
        [SerializeField] private Text hpCurrentText;
        [SerializeField] private Text bulletsCurrentText;
        [SerializeField] private Text bulletsMaximumText;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private PlayerController playerController;
        private int scoreValue;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            if (playerController == null)
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
        }

        #endregion public void

        #region private void

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
            playerController.AddEvent(() => SetValueHPFromPlayer());
            playerController.ShootController.AddEventToCurrentBulletsChange(() => SetValueBulletsCurrentFromPlayer());
            playerController.ShootController.AddEventToMaximumBulletsChange(() => SetValueBulletsMaximumFromPlayer());
        }

        private void SetValueHPFromPlayer()

        {
            hpCurrentText.text = playerController.GetHpPlayerCurrent().ToString();
            hpMaximumText.text = playerController.GetHpPlayerMaximum().ToString();
        }

        private void SetValueBulletsCurrentFromPlayer()
        {
            bulletsCurrentText.text = playerController.GetCountCurrentBulletsByCurrentWeapon().ToString();
        }

        private void SetValueBulletsMaximumFromPlayer()
        {
            bulletsMaximumText.text = playerController.GetCountMaxBulletsByCurrentWeapon().ToString();
        }

        private void GetValueFromScore()
        {
            scoreValue = int.Parse(scoreText.text);
        }

        private void SetValueToScore()
        {
            scoreText.text = scoreValue.ToString();
        }

        #endregion private void
    }
}