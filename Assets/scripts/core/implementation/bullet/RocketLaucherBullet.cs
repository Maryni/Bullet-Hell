using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class RocketLaucherBullet : BaseBullet
    {
        [SerializeField] private CircleCollider2D circleCollider2D;

        public override void Move(Vector2 pos, Transform pointForShooting)
        {
            Vector2 direction = pointForShooting.up;
            Rig2D.AddForce(direction * BulletStats.speed, ForceMode2D.Impulse);
        }

        private void Start()
        {
            StartCoroutine(ExplosiveByTime());
        }

        private void OnValidate()
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
        }

        public void ExplosiveByCollision()
        {

        }

        private IEnumerator ExplosiveByTime()
        {
            yield return new WaitForSeconds(Services.GetManager<DataManager>().DynamicData.RocketData.timeToBlowUp);

            float tempRadius = circleCollider2D.radius;
            circleCollider2D.radius = Services.GetManager<DataManager>().DynamicData.RocketData.radiutBlowUp;
            circleCollider2D.radius = tempRadius;
            gameObject.SetActive(false);

            yield return 0;
        }
    }
}