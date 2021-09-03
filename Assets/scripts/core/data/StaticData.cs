using System.Linq;
using UnityEngine;

namespace Global.Managers.Datas
{
    public enum WeaponType
    {
        AutomaticGun,
        Shotgun,
        RocketLaucher
    }

    public enum BulletType
    {
        AutomaticBullet,
        ShotgunBullet,
        RocketLaucherBullet
    }

    public enum EnemyType
    {
        MeleeGrounded_LowSpeed,
        MeleeGrounded_MiddleSpeed,
        MeleeGrounded_FastSpeed,
        MeleeFlyed,
        ShootingGrounded,
        ShootingFlyed
    }

    [CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData")]
    public class StaticData : ScriptableObject
    {
    }
}