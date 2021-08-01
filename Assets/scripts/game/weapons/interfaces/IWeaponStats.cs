using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Interfaces.Weapon
{
    public interface IWeaponStats
    {
        int BulletsCount { get; }
        float CooldownTime { get; }
        float Damage { get; }
        float ShootingRate { get; }
        int BulletOnShotUsed { get; }
    }
}