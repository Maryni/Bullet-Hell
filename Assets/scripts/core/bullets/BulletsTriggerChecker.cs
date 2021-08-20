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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == triggerType.ToString() && collision.isTrigger)
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
                        collision.GetComponent<PlayerController>().DamagePlayer(baseBullet.BulletStats.damage);
                    }
                    if (triggerType == TriggerType.Enemy)
                    {
                        collision.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == triggerType.ToString())
            {
                if (triggerType == TriggerType.Player)
                {
                }
            }
        }

        #endregion Unity function
    }
}