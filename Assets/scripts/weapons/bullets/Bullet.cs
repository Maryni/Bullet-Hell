using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//отсутствуют наймспейсы
//должен быть общий класс для пуль - BaseBullet
public class Bullet : MonoBehaviour
{
    #region private variables

    //ты не правильно поставил регион и не правильно используешь сериалайз филды, на них должны быть pragma warning (смотри в доке, что тебе Маша кидала)
    [SerializeField] private GameObject bullet;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float modSpeedBullet;
    [SerializeField] private Vector3 backupTransformVector3;
    [SerializeField] private int typeBullet;
    [SerializeField] private Transform parent;
    [SerializeField] private float timerToBlowUp;
    private bool isMoving = false;
    private float timerTemp;

    [SerializeField]
    private float modBlowUp = 1f; //прайвет и сериалайз должны быть отдельными, не правильная группировка

    #endregion private variables

    #region properties

    public int TypeBullet => typeBullet;

    public GameObject BulletObject =>
        bullet; //оно здесь вообще не нужно, у булета есть геймОбджект, нет смысла делать ссылку на объект

    #endregion properties

    #region public void

    public void Move(Vector2 pos)
    {
        Vector2 direction = pos - (Vector2) transform.position;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
        isMoving = true;
    }

    public void Move(Vector2 pos, Vector3 angle)
    {
        Vector2 direction = pos - (Vector2) transform.position;
        direction = direction * angle;
        rigidbody2D.AddForce(direction * modSpeedBullet, ForceMode2D.Impulse);
    }

    //функции которые не используешь - удаляем (больше того, она бесполезная, нового ничего она не делает)
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
        //хард код и фиксед апдейт
        //type bullet - должен быть реализован через enum
        //и каждый раз проверять иф в апдейте - не очень хорошо
        if (typeBullet == 2 && isMoving)
        {
            Explosion();
        }
    }

    //она написано не правильно, здесь используються магические числа
    private void SetBackupVectorPosition()
    {
        backupTransformVector3 = new Vector3(parent.localPosition.x + 0.1f, parent.localPosition.y + 0.1f, 0);
    }

    //глянь в сторону Coroutine в юнити и перепиши
    private void Explosion()
    {
        if (timerToBlowUp > 0)
        {
            timerToBlowUp -= 0.1f;
        }

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