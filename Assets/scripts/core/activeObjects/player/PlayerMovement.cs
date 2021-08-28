using UnityEngine;

namespace Global.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region private variables

#pragma warning disable
        [SerializeField] private Player player;
        [SerializeField] private Rigidbody2D rigidbody2D;
#pragma warning restore
        private Vector2 moveVelocity;

        #endregion private variables

        #region Unity function

        private void Update()
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveVelocity = moveInput.normalized * player.Speed;
        }

        private void FixedUpdate()
        {
            rigidbody2D.MovePosition(rigidbody2D.position + moveVelocity * Time.fixedDeltaTime);
        }

        #endregion Unity function
    }
}