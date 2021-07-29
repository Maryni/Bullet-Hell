using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponAction
{
    void Shot(Vector2 mousePos);

    void Reload();

    void LoadSettings();
}