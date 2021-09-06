using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class RocketLaucherBullet : BaseBullet //нейминг, ошибки в слове
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
            //каждый раз при сериализации в юнити оно будет его доставать
            circleCollider2D = GetComponent<CircleCollider2D>();
        }

        #endregion Unity functions

        #region public void

        public void ExplosiveRadiusUp()
        {
            //здесь у тебя ты можешь не получить нужные тебе значения
            //тут нужно считать разницу между ними и проверять на какое-то значение
            if (circleCollider2D.radius != Services.GetManager<DataManager>().DynamicData.RocketData.radiusBlowUp)
            {
                currentRadius = circleCollider2D.radius;
            }
            circleCollider2D.radius = Services.GetManager<DataManager>().DynamicData.RocketData.radiusBlowUp;
        }

        public void ExplosiveRadiusDown()
        {
            circleCollider2D.radius = currentRadius;
        }

        public override void Move()
        {
            ExplosiveRadiusDown();
            Rig2D.AddForce(transform.up * BulletStats.speed, ForceMode2D.Impulse);
            StartCoroutine(ExplosiveByTime());
            StartCoroutine(IncreaseSpeed());
        }

        #endregion public void

        #region private void

        private IEnumerator ExplosiveByTime()
        {
            yield return new WaitForSeconds(Services.GetManager<DataManager>().DynamicData.RocketData.timeToBlowUp);
            //мне нужны будут факты почему ты так написал
            //я бы вообще этот код удалил
            /*float tempRadius = circleCollider2D.radius;
            circleCollider2D.radius = Services.GetManager<DataManager>().DynamicData.RocketData.radiusBlowUp;
            circleCollider2D.radius = tempRadius;*/

            gameObject.SetActive(false);

            yield break; //не вызывается
        }

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

        #endregion private void
    }
}