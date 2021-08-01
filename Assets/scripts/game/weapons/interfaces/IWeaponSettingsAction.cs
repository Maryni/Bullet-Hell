using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Interfaces.Weapon
{
    public interface IWeaponSettingsAction
    {
        WeaponSettings WeaponSettings { get; }

        void LoadSettings();

        bool IsValuesChanged();
    }
}