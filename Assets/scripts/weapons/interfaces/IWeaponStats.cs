using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public interface IWeaponStats
{
    int BulletsCount { get; }
    float CooldownTime { get; }
    float Damage { get; }
    float ShootingRate { get; }
    int BulletOnShotUsed { get; }
}