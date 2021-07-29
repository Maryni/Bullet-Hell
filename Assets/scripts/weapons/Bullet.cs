using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region private variables

    [SerializeField] private GameObject bullet;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform backupTransform;
    [SerializeField] private float modSpeedBullet;
    [SerializeField] private int maxBullets;

    #endregion private variables

    #region properties

    public GameObject BulletObject => bullet;

    #endregion properties

    #region public void

    public void GetBackupTransform(Transform transform)
    {
        backupTransform = transform;
    }

    public void Move(Vector2 pos)
    {
        Vector2 direction = pos - (Vector2)transform.position;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
    }

    #endregion public void

    #region private void

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    #endregion private void
}