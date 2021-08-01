using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Shooting.BulletSpace
{
    public class BulletsWallChecker : MonoBehaviour
    {
        [SerializeField] private string tagForTrigget2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == tagForTrigget2D)
            {
                collision.GetComponent<Bullet>().GetBackToParent();
            }
        }
    }
}