using Global.ActiveObjects;
using Global.Controllers;
using Global.Managers.Datas;
using Global.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Upgrates
{
    public class PowerUp : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private TypePowerUp typePowerUp;
        [SerializeField] private Sprite damageSprite;
        [SerializeField] private Sprite defenceSprite;
        [SerializeField] private Sprite speedSprite;
        [SerializeField] private Sprite killAreaSprite;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CircleCollider2D coll2D;

        #endregion Inspector variables

        #region private variables

        private Coroutine coroutineDispawnObject;
        private GameController gameController;
        private List<GameObject> listTouched;
        private float startRadius;

        #endregion private variables

        #region Unity functions

        private void OnValidate()
        {
            if (coll2D == null)
            {
                coll2D = GetComponent<CircleCollider2D>();
            }
        }

        private void Start()
        {
            startRadius = coll2D.radius;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                gameController.SetPowerUpByType(typePowerUp, this);
            }
            else if (collision.gameObject.GetComponent<EnemyController>() && coll2D.radius == Services.GetManager<DataManager>().DynamicData.PowerUpSpawnItemData.killAreaRadius)
            {
                collision.gameObject.SetActive(false);
            }
            else if (collision == null && coll2D.radius == Services.GetManager<DataManager>().DynamicData.PowerUpSpawnItemData.killAreaRadius)
            {
                gameObject.SetActive(false);
                coll2D.radius = startRadius;
            }
        }

        #endregion Unity functions

        #region public void

        public void IncreaseRadius()
        {
            coll2D.radius = Services.GetManager<DataManager>().DynamicData.PowerUpSpawnItemData.killAreaRadius;
        }

        public void CheckAndSetGameController()
        {
            if (gameController == null)
            {
                gameController = FindObjectOfType<GameController>();
            }
        }

        public void DispawnObjectByTime(int time)
        {
            if (coroutineDispawnObject == null)
            {
                coroutineDispawnObject = StartCoroutine(DispawnObjectAtTime(time));
            }
        }

        public void SetPowerUpType(TypePowerUp typePowerUp)
        {
            this.typePowerUp = typePowerUp;
        }

        public void SetSprite()
        {
            if (typePowerUp == TypePowerUp.IncreaseDamage)
            {
                spriteRenderer.sprite = damageSprite;
            }
            else if (typePowerUp == TypePowerUp.IncreaseDefence)
            {
                spriteRenderer.sprite = defenceSprite;
            }
            else if (typePowerUp == TypePowerUp.IncreaseSpeed)
            {
                spriteRenderer.sprite = speedSprite;
            }
            else
            {
                spriteRenderer.sprite = killAreaSprite;
            }
        }

        #endregion public void

        #region private void

        private IEnumerator DispawnObjectAtTime(int time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
            StopCoroutineByCall(coroutineDispawnObject);
        }

        private void StopCoroutineByCall(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        #endregion private void
    }
}