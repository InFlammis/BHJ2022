using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement
{
    /// <summary>
    /// Implementation of a sound manager.
    /// It manages a sound pool as an implementation of the Pool pattern.
    /// The sound pool caches and reuse a number of AudioSource for the game sounds.
    /// When a sound has to be played, an instance of audioSource is requested from the pool.
    /// </summary>
    public class SoundManager : MyMonoBehaviour, ISoundManager
    {
        [SerializeField] private StaticObjectsSO _staticObjects;
        public StaticObjectsSO StaticObjects => _staticObjects;

        [SerializeField] private SoundManagerInitSettingsSO settings;

        private AudioSource _MusicSource;

        /// <summary>
        /// Audio source pool
        /// </summary>
        private AudioSourcePool _AudioSourcePool;

        void Awake()
        {
            _staticObjects.Messenger.PlayMusic.AddListener(PlayMusic);
            _staticObjects.Messenger.PlaySound.AddListener(PlaySound);

            _AudioSourcePool = new AudioSourcePool(this, settings.AudioSourcePoolSize);

            _MusicSource = gameObject.AddComponent<AudioSource>();
            _MusicSource.loop = true;
        }

        private void PlaySound(object publisher, string target, Sound sound)
        {
            var audioSource = _AudioSourcePool.GetAudioSource();
            audioSource.clip = sound.Clip;
            audioSource.volume = sound.Volume;
            audioSource.Play();
        }

        private void PlayMusic(object publisher, string target, Sound music)
        {
            if(_MusicSource.clip != null && music.Clip.name == _MusicSource.clip.name)
            {
                return;
            }

            _MusicSource.clip = music.Clip;
            _MusicSource.volume = music.Volume;
            _MusicSource.Play();
        }
    }
}
