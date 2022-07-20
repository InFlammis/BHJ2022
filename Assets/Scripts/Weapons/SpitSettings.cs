using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "New Spit Settings", menuName = "Game/Settings/Spit Settings")]
    public class SpitSettings : ScriptableObject
    {
        public string SpitName;

        public string Layer;
        public string Tag;

        /// <summary>
        /// Speed of the bullet
        /// </summary>
        [Range(0.01f, 1f)]
        public float Speed;

        /// <summary>
        /// Max distance reached by the bullet
        /// </summary>
        [Range(0.1f, 20)]
        public float Distance;

        /// <summary>
        /// Damage applied on collision
        /// </summary>
        public int Damage;

        /// <summary>
        /// Scale factor for the object size
        /// </summary>
        [Range(0, 1)]
        public float Scale;


    }
}
