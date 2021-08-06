using Global.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerChecker : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private string tagObject;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagObject)
        {
            collision.GetComponent<Player>().ObjectTriggered();
        }
    }

    #endregion Unity functions
}