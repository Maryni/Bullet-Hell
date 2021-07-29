using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponStats
{
    int BulletsCount { get; }
    float CooldownTime { get; }
    float Damage { get; }
    float ShootingRate { get; }

    void Shot(Vector2 mousePos);

    void Reload();
}