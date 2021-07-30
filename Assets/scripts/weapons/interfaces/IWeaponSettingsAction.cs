using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public interface IWeaponSettingsAction
{
    WeaponSettings WeaponSettings { get; }

    void LoadSettings();

    bool IsValuesChanged();
}