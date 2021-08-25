using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ActiveObjects
{
    public class EnemyMovement : MonoBehaviour
    {
        #region private variables

        private bool movementEnable = true;

        #endregion private variables

        #region public void

        public void Move(Transform transformPlayer, Rigidbody2D rig2D, float speed)
        {
            StartCoroutine(Movement(transformPlayer, rig2D, speed));
        }

        #endregion public void

        #region private void

        private IEnumerator Movement(Transform transformPlayer, Rigidbody2D rig2D, float speed)
        {
            if (transformPlayer != null)
            {
                while (movementEnable)
                {
                    transform.up = Rotation(transform, transformPlayer);
                    MovementRealize(transformPlayer, rig2D, speed);

                    yield return null;
                }
            }
        }

        private Vector2 Rotation(Transform transformObject, Transform transformPlayer)
        {
            return ((Vector2)transformPlayer.position - (Vector2)transformObject.position).normalized;
        }

        private void MovementRealize(Transform transformPlayer, Rigidbody2D rig2D, float speed)
        {
            Vector2 pos = transformPlayer.position - transform.position;
            rig2D.velocity = pos * speed * Time.deltaTime;
        }

        #endregion private void
    }
}