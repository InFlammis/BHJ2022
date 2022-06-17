using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.PowerUps
{
    /// <summary>
    /// Settings for a power-up
    /// </summary>
    [CreateAssetMenu(fileName = "New Power-Up Settings", menuName = "Game/Settings/Power-Up Settings")]
    public class PowerUpSettings : ScriptableObject
    {
        /// <summary>
        /// Power-up type
        /// </summary>
        public PowerUpType PowerUpType;

        /// <summary>
        /// Value carried over
        /// </summary>
        public float Value;

        /// <summary>
        /// Lifetime
        /// </summary>
        public float Duration;
    }
}
