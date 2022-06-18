using UnityEngine;
namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "New Spitter Settings", menuName = "Game/Settings/Spitter Settings")]
    public class SpitterSettings : ScriptableObject
    {
        public string SpitterName;

        /// <summary>
        /// Total number of spits
        /// </summary>
        public int InitSpitAmount;

        [Range(0f, 10f)]
        public float InterSpitInterval;

    }
}
