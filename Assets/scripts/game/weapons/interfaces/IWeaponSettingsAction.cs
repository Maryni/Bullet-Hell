using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponSettingsAction
{
    WeaponSettings WeaponSettings { get; }

    void LoadSettings();

    bool IsValuesChanged();
}