using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement
{
    /// <summary>
    /// A sound and its settings
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        /// <summary>
        /// Name of the sound
        /// </summary>
        public string Name;

        /// <summary>
        /// The clip associated to the sound
        /// </summary>
        public AudioClip Clip;

        /// <summary>
        /// The volume to play the sound at
        /// </summary>
        [Range(0f, 1f)]
        public float Volume;
    }
}
