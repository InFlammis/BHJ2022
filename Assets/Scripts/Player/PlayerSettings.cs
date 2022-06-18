using InFlammis.Victoria.Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Player
{
    /// <summary>
    /// Configuration for the Player
    /// </summary>
    [CreateAssetMenu(fileName = "New Player settings", menuName = "Game/Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Interactions")]
        /// <summary>
        /// Damage applied to enemies on collision
        /// </summary>
        public int DamageAppliedOnCollision;

        [Header("Movement")]
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

        [Header("Rotation")]
        [Range(0, 1)]
        public float RotationTolerance;

        [Range(0, 0.2f)]
        public float RotationSpeed;

        [Space()]
        /// <summary>
        /// Settings for weapons
        /// </summary>
        public List<SpitterSettings> SpitterSettings;
    }
}
