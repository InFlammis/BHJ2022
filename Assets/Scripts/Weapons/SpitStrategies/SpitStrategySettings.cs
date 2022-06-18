using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    /// <summary>
    /// Settings for a weapon
    /// </summary>
    [CreateAssetMenu(fileName = "New SpitStrategy Settings", menuName = "Game/Settings/SpitStrategy Settings")]
    public class SpitStrategySettings : ScriptableObject
    {
        public string SpitStrategyName;
        
        /// <summary>
        /// Type of weapon
        /// </summary>
        public float BurstWidth;

        /// <summary>
        /// Total number of ammo
        /// </summary>
        public int BurstSize;

        /// <summary>
        /// Speed at which the weapon fires up
        /// </summary>
        public float RateOfSpit;

    }
}
