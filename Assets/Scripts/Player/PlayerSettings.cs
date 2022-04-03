using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Weapons;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Player
{
    /// <summary>
    /// Configuration for the Player
    /// </summary>
    [CreateAssetMenu(fileName = "New Player settings", menuName = "Game/Init Settings/Player InitSettings")]
    public class PlayerSettings : ScriptableObject
    {
        /// <summary>
        /// Damage applied to enemies on collision
        /// </summary>
        public int DamageAppliedOnCollision;

        /// <summary>
        /// Max speed on movements
        /// </summary>
        [Range(0, 10)]
        public float MaxSpeed;

        /// <summary>
        /// Deceleration value when stopping
        /// </summary>
        [Range(0, 1)]
        public float Deceleration;

        /// <summary>
        /// Factor that controls the amount of force applied during movements
        /// </summary>
        public float ForceMultiplier;

        /// <summary>
        /// Settings for weapons
        /// </summary>
        public List<WeaponSettings> WeaponSettings;
    }
}
