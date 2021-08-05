using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Player;

namespace Global.ActiveObjects
{
    public class MeleeEnemy : BaseEnemy
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private Transform transformPlayer;
        [SerializeField] private Rigidbody2D rig2d;
        [SerializeField] private float timeInvokeReapeating;
        [SerializeField] private float rateInvokeReapeatingMovement;
        [SerializeField] private float rateInvokeReapeatingReset;
#pragma warning restore

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            Init();
            InvokeRepeating("ResetVelocity", timeInvokeReapeating, rateInvokeReapeatingReset); //for deleting "curve" when player moving, and enemy "drifting" in circle around player
            InvokeRepeating("Movement", timeInvokeReapeating, rateInvokeReapeatingMovement);
        }

        private void OnValidate()
        {
            rig2d = GetComponent<Rigidbody2D>();
        }

        #endregion Unity functions

        #region public void

        public override void Movement()
        {
            SetPlayerTransform();
            transform.rotation = Rotation(transformPlayer.position, transform);
            Vector2 pos = transformPlayer.position - transform.position;
            rig2d.AddForce(pos * EnemyStats.speed, ForceMode2D.Force);
        }

        public override void Attack()
        {
            Debug.Log("Attacking");
        }

        #endregion public void

        #region private void

        private void ResetVelocity() => rig2d.velocity = Vector2.zero;

        private void SetPlayerTransform() => transformPlayer = FindObjectOfType<Player.Player>().transform;

        private Quaternion Rotation(Vector3 mousePos, Transform transformObject)
        {
            float AngleRad = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x);
            AngleRad = (180 / Mathf.PI) * AngleRad;
            AngleRad += 90;
            AngleRad += 180;
            return transformObject.rotation = Quaternion.Euler(0, 0, AngleRad);
        }

        #endregion private void
    }
}