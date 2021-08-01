using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Interfaces.Weapon
{
    public interface IWeapon : IWeaponAction, IWeaponStats, IWeaponSettingsAction
    {
    }
}