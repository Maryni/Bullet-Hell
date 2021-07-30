using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//нет неймспейса

//у тебя в целом неправильное интерйфесов
//пока ты не используешь их (кроме реализации) - они бесполезны
//либо их нужно удалить, либо переписать

//это касается и их IWeaponAction, IWeaponStats, IWeaponSettingsAction
public interface IWeapon : IWeaponAction, IWeaponStats, IWeaponSettingsAction
{
}