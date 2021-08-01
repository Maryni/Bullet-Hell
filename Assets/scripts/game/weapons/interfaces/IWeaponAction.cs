using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Interfaces.Weapon
{
    public interface IWeaponAction
    {
        void Shot(Vector2 mousePos);

        void Reload();

        void LoadSettings();
    }
}