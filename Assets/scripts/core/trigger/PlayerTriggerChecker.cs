using Global.ActiveObjects;
using Global.Player;
using System;
using UnityEngine;

namespace Global.Trigger
{
    public class PlayerTriggerChecker : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private string tagObject;
        [SerializeField] private bool canAttack;
        [SerializeField] private GameObject player;
#pragma warning restore

        #endregion Inspector variables

        #region private variables

        private Action action;

        #endregion private variables

        #region public void

        public void EnableAttack()
        {
            canAttack = true;
        }

        public void DisableAttack()
        {
            canAttack = false;
        }

        public GameObject GetPlayer() => player;

        public void AddEvent(Action action)
        {
            this.action += action;
        }

        public void RestoreEvents()
        {
            action = null;
        }

        #endregion public void

        #region Unity functions

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.GetComponent<PlayerController>())
            {
                if (canAttack)
                {
                    if (player == null || player != collision.gameObject)
                    {
                        player = collision.gameObject;

                        if (gameObject.activeInHierarchy)
                        {
                            action?.Invoke();
                        }
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.GetComponent<PlayerController>())
            {
                gameObject.GetComponentInParent<EnemyController>().DisableAttack();
                action?.Invoke();
            }
        }

        #endregion Unity functions
    }
}