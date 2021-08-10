using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class RocketLaucherBullet : BaseBullet
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private CircleCollider2D circleCollider2D;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void OnValidate()
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
        }

        #endregion Unity functions

        #region public void

        public void Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        }

        public override void Move()
        {
            Rig2D.AddForce(transform.up * BulletStats.speed, ForceMode2D.Impulse);
            StartCoroutine(ExplosiveByTime());
        }

        #endregion public void

        #region private void

        private IEnumerator ExplosiveByTime()
        {
            yield return new WaitForSeconds(Services.GetManager<DataManager>().DynamicData.RocketData.timeToBlowUp);
            float tempRadius = circleCollider2D.radius;
            circleCollider2D.radius = Services.GetManager<DataManager>().DynamicData.RocketData.radiutBlowUp;
            circleCollider2D.radius = tempRadius;

            gameObject.SetActive(false);

            yield return 0;
        }

        #endregion private void
    }
}