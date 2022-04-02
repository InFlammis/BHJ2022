using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Weapons
{
    /// <summary>
    /// Settings for a bullet
    /// </summary>
    [CreateAssetMenu(fileName = "New Bullet InitSettings", menuName = "Game/Init Settings/Bullet InitSettings")]
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
