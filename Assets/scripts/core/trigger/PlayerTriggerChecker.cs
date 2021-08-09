using Global.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerChecker : MonoBehaviour
{
    #region Inspector variables

#pragma warning disable
    [SerializeField] private string tagObject;
    [SerializeField] private bool canAttack;
    [SerializeField] private GameObject player;
#pragma warning restore

    #endregion Inspector variables

    #region public void

    public void EnableAttack() => canAttack = true;

    public void DisableAttacl() => canAttack = false;

    public GameObject GetPlayer() => player;

    #endregion public void

    #region Unity functions

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == tagObject)
        {
            if (tagObject == "Player" && canAttack)
            {
                if (player == null || player != collision.gameObject)
                {
                    player = collision.gameObject;
                    Debug.Log($"[ {collision.name} ] are triggered by me [ {name} ]");
                }
            }
        }
    }

    #endregion Unity functions
}