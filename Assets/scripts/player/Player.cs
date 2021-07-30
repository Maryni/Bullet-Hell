using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public class Player : MonoBehaviour, IPlayerStats
{
    #region private variables

    [SerializeField] private int hp = 100;
    [SerializeField] private float speed;

    #endregion private variables

    #region properties

    public int HP => hp;
    public float Speed => speed;

    #endregion properties

    #region public void

    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    #endregion public void
}