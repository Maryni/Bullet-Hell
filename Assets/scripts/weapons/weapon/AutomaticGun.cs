using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public class AutomaticGun : MonoBehaviour, IWeapon //неправильная архитектура интерфейсов, двойная реализация WeaponSettings
{
    #region private variables
    //ты не правильно поставил регион и не правильно используешь сериалайз филды, на них должны быть pragma warning (смотри в доке, что тебе Маша кидала)

    [SerializeField] private ShootManager shootManager; //не используется - удалить

    [Header("Not change 0 if we have to load settings")] [SerializeField]
    private int bulletCount;

    [SerializeField] private int currentBullets;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;
    [SerializeField] private float shotingRate;
    [SerializeField] private int bulletOnShotUsed;
    [Space, SerializeField] private WeaponSettings weaponSettings;
    private bool isReloading;

    [SerializeField] private Bullet bullet;

    #endregion private variables

    #region properties

    public int BulletsCount => bulletCount;
    public float CooldownTime => cooldownTime;
    public float Damage => damage;
    public float ShootingRate => shotingRate;
    public int BulletOnShotUsed => bulletOnShotUsed;
    public WeaponSettings WeaponSettings => weaponSettings;
    public Bullet Bullet => bullet;

    #endregion properties

    #region public void

    public void GetBullet(GameObject bulletObject) //неправильный нейминг + ты можешь передать не геймобджект, а скрипт
    {//гет - получить, сет - задать
        //в данном случае - ты задаешь
        bullet = bulletObject.GetComponent<Bullet>();
    }

    //откуда подгружаются эти значения, где они указаны - не ясно + если значение не 0 будут в самом начале,
    //а значения мы изменили - оно не сработает, и данные будут прежними, а где-то изменены
    public void LoadSettings()
    {
        if (!IsValuesChanged())
        {
            bulletCount = WeaponSettings.BulletsCount;
            cooldownTime = WeaponSettings.CooldownTime;
            damage = WeaponSettings.Damage;
            shotingRate = WeaponSettings.ShootingRate;
        }
    }

    //посмотри в сторону Coroutine
    public void Reload()
    {
        float timer = cooldownTime;
        isReloading = true;
        while (timer > 0)
        {
            timer -= 0.1f;
            Debug.Log("Reloading " + this.gameObject.name);
        }

        if (timer < 0)
        {
            isReloading = false;
            currentBullets = bulletCount;
        }

        Debug.Log("Reloading finished " + this.gameObject.name);
    }

    public void Shot(Vector2 mousePos)
    {
        bullet.BulletObject.SetActive(true);
        if (currentBullets <= 0)
        {
            Reload();
        }

        if (!isReloading)
            bullet.Move(mousePos);
        currentBullets--;
    }

    //она не нужна
    public bool IsValuesChanged()
    {
        if (bulletCount == 0 && cooldownTime == 0 && damage == 0 && shotingRate == 0) //????????
            return false;
        else //скобки не забывай + можно без else
            return true;

        //if(true==true)
        //return true; //????????

        //ты можешь написать return bulletCount == 0 && cooldownTime == 0 && damage == 0 && shotingRate == 0
    }

    #endregion public void

    #region private void

    private void Start()
    {
        LoadSettings();
        currentBullets = bulletCount;
    }

    //к чему относить этот самари? под ним ничего нет, и лучше почитай для чего он используеться, что не использовать его просто так
    /// <summary>
    /// if we didn't write settings in Inspector
    /// </summary>

    #endregion private void
}

//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// а теперь посмотри на свой код с оружием
// тебе не кажеться, что все три скрипта похожи между собой?
// много одинаковой реализации, много повторений?
// почему бы тебе не переписать это более правильно?
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!