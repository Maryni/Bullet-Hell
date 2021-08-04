using Global.Managers.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Bullet
{
    public class RocketLaucherBullet : BaseBullet
    {
        [SerializeField] private CircleCollider2D circleCollider2D;
        [SerializeField] private float timeToBlowUp;

        public override void Move(Vector2 pos, Transform pointForShooting)
        {
            Vector2 direction = pointForShooting.up;
            Rig2D.AddForce(direction * BulletStats.speed, ForceMode2D.Impulse);
            StartCoroutine(Explosive());
        }

        private void Start()
        {
            timeToBlowUp = Services.GetManager<DataManager>().DynamicData.RocketData.timeToBlowUp;
        }

        private void OnValidate()
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private IEnumerator Explosive()
        {
            if (timeToBlowUp > 0)
            {
                timeToBlowUp -= 0.1f;
            }
            if (timeToBlowUp < 0)
            {
                float tempRadius = circleCollider2D.radius;
                circleCollider2D.radius = Services.GetManager<DataManager>().DynamicData.RocketData.radiutBlowUp;
                circleCollider2D.radius = tempRadius;
                timeToBlowUp = Services.GetManager<DataManager>().DynamicData.RocketData.timeToBlowUp;
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}