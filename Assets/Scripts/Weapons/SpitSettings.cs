using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "New Spit Settings", menuName = "Game/Settings/Spit Settings")]
    public class SpitSettings : ScriptableObject
    {
        public string SpitName;

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
