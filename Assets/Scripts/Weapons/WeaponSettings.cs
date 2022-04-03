using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Weapons
{
    /// <summary>
    /// Settings for a weapon
    /// </summary>
    [CreateAssetMenu(fileName = "New Weapon InitSettings", menuName = "Game/Init Settings/Weapon InitSettings")]
    public class WeaponSettings : ScriptableObject
    {
        /// <summary>
        /// Type of weapon
        /// </summary>
        public WeaponType WeaponType;

        /// <summary>
        /// Magazine capacity
        /// </summary>
        public int MagazineCapacity;

        /// <summary>
        /// Total number of ammo
        /// </summary>
        public int Ammo;

        /// <summary>
        /// Speed at which the weapon fires up
        /// </summary>
        public float RateOfFire;

    }
}
