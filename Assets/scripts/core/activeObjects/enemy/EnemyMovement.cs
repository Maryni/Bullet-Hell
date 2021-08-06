using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class EnemyMovement : MonoBehaviour
    {
        #region public void

        public void Movement(Transform transformPlayer, Rigidbody2D rig2D, float speed)
        {
            Vector2 pos = transformPlayer.position - transform.position;
            rig2D.AddForce(pos * speed, ForceMode2D.Force);
        }

        #endregion public void
    }
}