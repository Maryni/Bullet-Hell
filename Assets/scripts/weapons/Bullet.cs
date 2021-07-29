using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region private variables

    [SerializeField] private GameObject bullet;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float modSpeedBullet;
    [SerializeField] private Vector3 backupTransformVector3;

    #endregion private variables

    #region properties

    public GameObject BulletObject => bullet;

    #endregion properties

    #region public void

    public void Move(Vector2 pos)
    {
        Vector2 direction = pos - (Vector2)transform.position;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
    }

    public void Move(Vector2 pos, Vector3 angle)
    {
        Vector2 direction = pos - (Vector2)transform.position;
        direction = direction * angle;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
    }

    #endregion public void

    #region private void

    private void Start()
    {
        backupTransformVector3 = this.gameObject.transform.localPosition;
    }

    private void OnBecameInvisible()
    {
        gameObject.transform.position = backupTransformVector3;
        this.gameObject.SetActive(false);
    }

    #endregion private void
}