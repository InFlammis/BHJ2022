using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.SoundManagement
{
    /// <summary>
    /// Implementation of a sound manager.
    /// It manages a sound pool as an implementation of the Pool pattern.
    /// The sound pool caches and reuse a number of AudioSource for the game sounds.
    /// When a sound has to be played, an instance of audioSource is requested from the pool.
    /// </summary>
    public class SoundManager : MyMonoBehaviour, ISoundManager
    {
        [SerializeField]
        private AudioClip _MenuMusic;
        [SerializeField]
        private AudioClip _GameMusic;

        /// <summary>
        /// Audio clip for the menu music
        /// </summary>
        public AudioClip MenuMusic => _MenuMusic;

        /// <summary>
        /// Audio clip for the game music
        /// </summary>
        public AudioClip GameMusic => _GameMusic;

        private AudioSource _MusicSource;

        /// <summary>
        /// Audio source pool
        /// </summary>
        private AudioSourcePool _AudioSourcePool;

        /// <summary>
        /// Size of the pool
        /// </summary>
        [SerializeField]
        private int _AudioSourcePoolSize = 10;

        void Awake()
        {
            _AudioSourcePool = new AudioSourcePool(this, _AudioSourcePoolSize);

            _MusicSource = gameObject.AddComponent<AudioSource>();
            _MusicSource.loop = true;
        }
        
        /// <summary>
        /// Play an audioclip for music
        /// </summary>
        /// <param name="audioClip"></param>
        public void PlayMusic(AudioClip audioClip)
        {
            _MusicSource.clip = audioClip;
            _MusicSource.volume = 0.1f;
            _MusicSource.Play();
        }

        /// <summary>
        /// Play a sound
        /// </summary>
        /// <param name="audioClip">Audio clip to play</param>
        public void PlaySound(AudioClip audioClip)
        {
            var audioSource = _AudioSourcePool.GetAudioSource();
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        /// <summary>
        /// play a sound
        /// </summary>
        /// <param name="sound">Sound to play</param>
        public void PlaySound(Sound sound)
        {
            var audioSource = _AudioSourcePool.GetAudioSource();
            audioSource.clip = sound.Clip;
            audioSource.volume = sound.Volume;
            audioSource.Play();
        }
    }
}
