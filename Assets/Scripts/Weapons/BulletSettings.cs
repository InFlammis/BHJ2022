﻿using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    /// <summary>
    /// Settings for a bullet
    /// </summary>
    [CreateAssetMenu(fileName = "New Bullet Settings", menuName = "Game/Settings/Bullet Settings")]
    public class BulletSettings : ScriptableObject
    {
        /// <summary>
        /// Weapon type the bullet applies to
        /// </summary>
        public WeaponType WeaponType;

        /// <summary>
        /// Speed of the bullet
        /// </summary>
        public float Speed;

        /// <summary>
        /// Damage applied on collision
        /// </summary>
        public int Damage;

        /// <summary>
        /// Scale factor for the object size
        /// </summary>
        public float Scale;
    }
}
