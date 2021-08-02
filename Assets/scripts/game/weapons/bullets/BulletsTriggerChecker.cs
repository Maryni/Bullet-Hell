using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Shooting.BulletSpace
{
    public class BulletsTriggerChecker : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private string tagForTrigget2D;
#pragma warning restore

        #endregion private variables

        #region private void

        #region Unity function

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == tagForTrigget2D)
            {
                collision.GetComponent<Bullet>().GetBackToParent();
            }
        }

        #endregion Unity function

        #endregion private void
    }
}