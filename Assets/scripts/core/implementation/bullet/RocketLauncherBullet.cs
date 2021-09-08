using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class RocketLauncherBullet : BaseBullet
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private CircleCollider2D circleCollider2D;
#pragma warning restore

        #endregion Inspector variables

        #region properties

        public float CurrentRadius => circleCollider2D.radius;

        #endregion properties

        #region private variables

        private float currentRadius;

        #endregion private variables

        #region Unity functions

        private void OnValidate()
        {
            if (circleCollider2D == null)
            {
                circleCollider2D = GetComponent<CircleCollider2D>();
            }
        }

        #endregion Unity functions

        #region public void

        public override void TriggetBulletOnCollision()
        {
            if (coroutineOnCollisionCalled == null)
            {
                Debug.Log("Coroutine started");
                coroutineOnCollisionCalled = StartCoroutine(TriggerOnCollisionRealization(coroutineOnCollisionCalled));
            }
        }

        private IEnumerator TriggerOnCollisionRealization(Coroutine coroutine)
        {
            ExplosiveRadiusUp();
            yield return null;
            ExplosiveRadiusDown();

            base.TriggetBulletOnCollision();
        }

        public void ExplosiveRadiusUp()
        {
            if (circleCollider2D.radius != Services.GetManager<DataManager>().DynamicData.RocketData.radiusBlowUp)
            {
                currentRadius = circleCollider2D.radius;
                circleCollider2D.radius = Services.GetManager<DataManager>().DynamicData.RocketData.radiusBlowUp;
            }
        }

        public void ExplosiveRadiusDown()
        {
            if (currentRadius > 0.5f)
            {
                circleCollider2D.radius = currentRadius;
            }
        }

        public override void Move()
        {
            ExplosiveRadiusDown();
            Rig2D.AddForce(transform.up * BulletStats.speed, ForceMode2D.Impulse);
            StartCoroutine(IncreaseSpeed());
            StartCoroutine(DisableByTime(Services.GetManager<DataManager>().DynamicData.RocketData.timeToBlowUp));
        }

        private IEnumerator DisableByTime(float time)
        {
            yield return new WaitForSeconds(time);
            ExplosiveRadiusUp();
            yield return null;
            ExplosiveRadiusDown();
            gameObject.SetActive(false);
        }

        #endregion public void

        #region private void

        private IEnumerator IncreaseSpeed()
        {
            var data = Services.GetManager<DataManager>().DynamicData.RocketData;
            float startSpeed = data.minSpeed;
            float finishSpeed = data.maxSpeed;
            float currentSpeed = startSpeed;
            float step = (data.maxSpeed - data.minSpeed) / data.timeAcceleration;
            while (currentSpeed <= finishSpeed)
            {
                Rig2D.velocity = Vector2.zero;
                Rig2D.AddForce(transform.up * currentSpeed, ForceMode2D.Impulse);
                currentSpeed += step * Time.deltaTime;
                yield return null;
            }
        }

        //private IEnumerator ExplosiveWithDelay()
        //{
        //    yield return new WaitForEndOfFrame();
        //    if (listTouchingObjects.Count > 0)
        //    {
        //        foreach (GameObject gameObjectItem in listTouchingObjects)
        //        {
        //            if (gameObjectItem.activeInHierarchy)
        //            {
        //                gameObjectItem.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
        //            }
        //        }

        //        listTouchingObjects.Clear();
        //        gameObject.GetComponent<RocketLauncherBullet>().ExplosiveRadiusDown();
        //    }
        //    gameObject.SetActive(false);
        //}

        #endregion private void
    }
}