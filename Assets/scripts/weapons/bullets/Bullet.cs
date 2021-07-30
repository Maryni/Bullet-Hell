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
    [SerializeField] private int typeBullet;
    [SerializeField] private Transform parent;
    [SerializeField] private float timerToBlowUp;
    private bool isMoving = false;
    private float timerTemp;
    [SerializeField] private float modBlowUp = 1f;

    #endregion private variables

    #region properties

    public int TypeBullet => typeBullet;
    public GameObject BulletObject => bullet;

    #endregion properties

    #region public void

    public void Move(Vector2 pos)
    {
        Vector2 direction = pos - (Vector2)transform.position;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
        isMoving = true;
    }

    public void Move(Vector2 pos, Vector3 angle)
    {
        Vector2 direction = pos - (Vector2)transform.position;
        direction = direction * angle;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
    }

    public void SetParent(GameObject gameObject)
    {
        parent = gameObject.transform;
    }

    public void GetBackToParent()
    {
        SetBackupVectorPosition();
        gameObject.transform.position = backupTransformVector3;
        this.gameObject.SetActive(false);
    }

    #endregion public void

    #region private void

    private void Start()
    {
        backupTransformVector3 = this.gameObject.transform.localPosition;
        timerTemp = timerToBlowUp;
    }

    private void FixedUpdate()
    {
        if (typeBullet == 2 && isMoving)
        {
            Explosion();
        }
    }

    private void SetBackupVectorPosition()
    {
        backupTransformVector3 = new Vector3(parent.localPosition.x + 0.1f, parent.localPosition.y + 0.1f, 0);
    }

    private void Explosion()
    {
        timerToBlowUp -= 0.1f;
        if (timerToBlowUp < 0)
        {
            float temp = gameObject.GetComponent<CircleCollider2D>().radius;
            gameObject.GetComponent<CircleCollider2D>().radius = temp * modBlowUp;
            GetBackToParent();
            isMoving = false;
            gameObject.GetComponent<CircleCollider2D>().radius = temp;
            timerToBlowUp = timerTemp;
        }
    }

    #endregion private void
}