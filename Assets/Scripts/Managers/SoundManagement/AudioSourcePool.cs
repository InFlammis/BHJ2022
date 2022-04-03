using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.SoundManagement
{
    /// <summary>
    /// Implementation of a Pool pattern to manage audio sources efficiently.
    /// Multiple sounds are continuously played during a game.
    /// This pattern is to avoid continuously creating and destroying audio source objects.
    /// When a sound needs to be played, the pool looks if there is an instance available (not busy playing another sound).
    /// If there is, it returns it. If no audio source is available but the pool size is below the max limit, a new Audio Source instance is created and returned.
    /// This new instance is then added to the pool and reused.
    /// If there are no AudioSource available and the pool is full, then a random AudioSource is returned.
    /// </summary>
    public class AudioSourcePool
    {
        /// <summary>
        /// Reference to the SoundManager instance
        /// </summary>
        private ISoundManager _SoundManager;

        /// <summary>
        /// Size of the pool
        /// </summary>
        private int _DefaultPoolSize = 10;

        /// <summary>
        /// The pool cache is actually a list of AudioSources
        /// </summary>
        private List<AudioSource> audioSources = new List<AudioSource>();

        /// <summary>
        /// The maximum pool size
        /// </summary>
        public int MaxPoolSize { get; set; }

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="soundManager">Reference to the <see cref="ISoundManager"/> instance</param>
        /// <param name="maxPoolSize">Maximum size of the pool</param>
        public AudioSourcePool(ISoundManager soundManager, int maxPoolSize)
        {
            if (soundManager == null)
                Debug.LogError("SoundManager is null");

            _SoundManager = soundManager ;

            if(maxPoolSize <= 0)
            {
                Debug.LogWarning($"maxPoolSize must be greater than 0. Setting it to {_DefaultPoolSize}.");
                MaxPoolSize = _DefaultPoolSize;
            }
            MaxPoolSize = maxPoolSize;
        }

        /// <summary>
        /// Return an available audio source from the pool
        /// </summary>
        /// <returns></returns>
        public AudioSource GetAudioSource()
        {
            var audioSource = audioSources.FirstOrDefault(x=>x.isPlaying == false);

            if (audioSource == null)
            {
                if (audioSources.Count == MaxPoolSize)
                    return audioSources[(int)(UnityEngine.Random.value * audioSources.Count)];

                audioSource = _SoundManager.GameObject.AddComponent<AudioSource>();
                audioSources.Add(audioSource);
            }
            return audioSource;
        }
    }
}
