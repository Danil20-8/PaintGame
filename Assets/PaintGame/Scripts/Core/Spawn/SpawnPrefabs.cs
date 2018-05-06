using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using PaintGame.Character;
using UnityEngine;
using PaintGame.Weapons;

namespace PaintGame.Core.Spawn
{
    [Serializable]
    public class SpawnPrefabs : ISpawnPrefabs
    {
        public PlayerPrefabs PlayerPrefabs;
        public WeaponPrefabs WeaponPrefabs;
    }

    [Serializable]
    public class PlayerPrefabs : ISpawnPrefabs
    {
        public PlayerCharacter Player;
    }

    [Serializable]
    public class WeaponPrefabs : ISpawnPrefabs
    {
        public Weapon Weapon;
    }
}
