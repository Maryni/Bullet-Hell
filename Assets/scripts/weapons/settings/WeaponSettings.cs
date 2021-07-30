using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Weapon/Settings", order = 4)]
public class WeaponSettings : ScriptableObject, IWeaponStats
{
    [SerializeField] private string weaponName;
    [SerializeField] private int bulletCount;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;
    [SerializeField] private float shooringRate;
    [SerializeField] private int bulletOnShotUsed;
    [SerializeField] private int countActiveBulletsOnScene;

    public string WeaponName => weaponName;
    public int BulletsCount => bulletCount;

    public float CooldownTime => cooldownTime;

    public float Damage => damage;

    public float ShootingRate => shooringRate;
    public int BulletOnShotUsed => bulletOnShotUsed;
    public int CountActiveBulletsOnScene => countActiveBulletsOnScene;
}