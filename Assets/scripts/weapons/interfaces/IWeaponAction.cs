using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса
public interface IWeaponAction
{
    void Shot(Vector2 mousePos);

    void Reload();

    void LoadSettings();
}