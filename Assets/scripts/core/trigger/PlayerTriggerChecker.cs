using Global.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerChecker : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private string tagObject;
    [SerializeField] private bool canAttack;

    #endregion Inspector variables

    #region public void

    public void EnableAttack() => canAttack = true;

    public void DisableAttacl() => canAttack = false;

    #endregion public void

    #region Unity functions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagObject)
        {
            if (tagObject == "Player" && canAttack)
            {
                collision.GetComponent<Player>().StartCoroutine("ObjectTriggered");
                Debug.Log($"[ {collision.name} ] are triggered by me [ {name} ]");
            }
        }
    }

    #endregion Unity functions
}