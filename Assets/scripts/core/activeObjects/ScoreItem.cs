using Global.Controllers;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class ScoreItem : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private int minValueScore;
        [SerializeField] private int maxValueScore;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private HUDController hUDController;

        #endregion private variables

        #region public void

        public void FindAndSetHUDController()
        {
            if (hUDController == null)
            {
                hUDController = FindObjectOfType<HUDController>();
            }
            SubscribeOnDieEnemy();
        }

        #endregion public void

        #region private void

        private void AddScore()
        {
            hUDController.AddScore(Random.Range(minValueScore, maxValueScore + 1));
        }

        private void SubscribeOnDieEnemy()
        {
            GetComponent<EnemyController>().AddEventToEnemy(() => AddScore());
        }

        #endregion private void
    }
}