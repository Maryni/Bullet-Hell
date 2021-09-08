using Global.ActiveObjects;
using Global.Player;
using UnityEngine;
using Global.Bullet;
using System.Collections.Generic;
using System.Collections;

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

        #region private variables

        private List<GameObject> listTouchingObjects = new List<GameObject>();

        #endregion private variables

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
            if (collision.GetComponent<EnemyController>())
            {
                if (!listTouchingObjects.Contains(collision.gameObject))
                {
                    listTouchingObjects.Add(collision.gameObject);
                }
                if (baseBullet.BulletStats.bulletType == Managers.Datas.BulletType.RocketLaucherBullet)
                {
                    ((RocketLauncherBullet)baseBullet).ExplosiveRadiusUp();
                    StartCoroutine(ExplosiveWithDelay());
                }
                else
                {
                    collision.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                    gameObject.SetActive(false);
                }
            }
            if (collision.GetComponent<BulletsTriggerChecker>() && collision.GetComponent<BulletsTriggerChecker>().triggerType == TriggerType.Bullet)
            {
                gameObject.SetActive(false);
            }

            #endregion Unity function
        }

        private IEnumerator ExplosiveWithDelay()
        {
            yield return new WaitForEndOfFrame();
            if (baseBullet.BulletStats.bulletType == Managers.Datas.BulletType.RocketLaucherBullet && listTouchingObjects.Count > 0)
            {
                foreach (GameObject gameObjectItem in listTouchingObjects)
                {
                    if (gameObjectItem.activeInHierarchy)
                    {
                        gameObjectItem.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                    }
                }

                listTouchingObjects.Clear();
                gameObject.GetComponent<RocketLauncherBullet>().ExplosiveRadiusDown();
            }
            gameObject.SetActive(false);
        }
    }
}