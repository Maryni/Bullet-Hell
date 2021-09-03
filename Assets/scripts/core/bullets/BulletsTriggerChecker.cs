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
                    ((RocketLaucherBullet)baseBullet).ExplosiveRadiusUp();
                    collision.gameObject.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                    StartCoroutine(LateCall());
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

        private IEnumerator LateCall()
        {
            yield return new WaitForFixedUpdate();
            if (baseBullet.BulletStats.bulletType == Managers.Datas.BulletType.RocketLaucherBullet && listTouchingObjects.Count > 0)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                foreach (GameObject gameObjectItem in listTouchingObjects)
                {
                    if (gameObjectItem.activeInHierarchy)
                    {
                        gameObjectItem.gameObject.GetComponent<EnemyController>().DamageEnemy(baseBullet.BulletStats.damage);
                    }
                }

                gameObject.SetActive(false);
                for (int i = 0; i < listTouchingObjects.Count; i++)
                {
                    if (!listTouchingObjects[i].activeInHierarchy)
                    {
                        listTouchingObjects.RemoveAt(i);
                    }
                }
                ((RocketLaucherBullet)baseBullet).ExplosiveRadiusDown();
            }
            yield break;
        }
    }
}