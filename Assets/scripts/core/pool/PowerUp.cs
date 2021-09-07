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

        #endregion Inspector variables

        #region private variables

        private Coroutine coroutineDispawnObject;
        private GameObject player;

        #endregion private variables

        #region Unity functions

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
            }
        }

        #endregion Unity functions

        #region public void

        public void CheckAndSetPlayerTransform()
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>().gameObject;
            }
        }

        public void DispawnObjectByTime(int time)
        {
            if (coroutineDispawnObject == null)
            {
                coroutineDispawnObject = StartCoroutine(DispawnObjectAtTime(time));
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