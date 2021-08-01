using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Interfaces.Player
{
    public interface IPlayerStats
    {
        int HP { get; }
        float Speed { get; }
    }
}