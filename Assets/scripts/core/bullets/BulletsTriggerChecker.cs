using Global.ActiveObjects;
using Global.Player;
using UnityEngine;
using Global.Bullet;

namespace Global.Shooting.BulletSpace
{
    public enum TriggerType
    {
        Player,
        Enemy,
        Bullet
    }

    public class BulletsTriggerChecker : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private TriggerType triggerType;
        [SerializeField] private bool useMeOrTriggerObject; //true - me, false - triggerObject
        [SerializeField] private bool dealDamage;
        [SerializeField] private bool mustDisable;
        [SerializeField] private BaseBullet baseBullet;
#pragma warning restore

        #endregion Inspector variables

        #region Unity function

        private void OnValidate()
        {
            if (baseBullet == null)
            {
                baseBullet = GetComponent<BaseBullet>();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == triggerType.ToString())
            {
                if (!useMeOrTriggerObject)
                {
                    if (mustDisable)
                    {
                        collision.gameObject.SetActive(false);
                    }
                }
                if (useMeOrTriggerObject)
                {
                    if (mustDisable)
                    {
                        gameObject.SetActive(false);
                    }
                }
                if (dealDamage)
                {
                    if (triggerType == TriggerType.Player)
                    {
                        collision.gameObject.GetComponent<PlayerController>().DamagePlayer(baseBullet.BulletStats.damage);
                    }
                    if (triggerType == TriggerType.Enemy && collision.gameObject.tag != TriggerType.Bullet.ToString())
                    {
                        if (baseBullet.BulletStats.bulletType == Managers.Datas.BulletType.RocketLaucherBullet)
                        {
                            ((RocketLaucherBullet)baseBullet).ExplosiveRadiusUp();
                            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                            ((RocketLaucherBullet)baseBullet).ExplosiveRadiusDown();
                        }
                        else if (collision.gameObject.GetComponent<EnemyController>())
                        {
                            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                        }
                        else
                        {
                            collision.gameObject.GetComponentInParent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                        }
                    }
                }
            }
        }

        #endregion Unity function
    }
}