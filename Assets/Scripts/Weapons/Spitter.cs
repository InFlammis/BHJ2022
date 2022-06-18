using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using System;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public class Spitter :  MyMonoBehaviour
    {
        [Header("Dependencies", order = 1)]
        [SerializeField]
        private StaticObjectsSO _staticObjects;
        /// <summary>
        /// Reference to the GameObject used as a spit
        /// </summary>
        public GameObject Spit;

        /// <summary>
        /// Initial settings for the spitter
        /// </summary>
        [Header("Settings", order = 2)]
        public SpitterSettings InitSettings;

        [Header("Sounds", order = 4)]
        [SerializeField] 
        private Sound SpitSound;

        [Header("Runtime values", order = 4)]
        /// <summary>
        /// Current amount of spits
        /// </summary>
        public int CurrentSpitAmount;

        private SpitStrategy SpitStrategy;



        void Awake()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException($"{nameof(InitSettings)} cannot be null for Spitter.");
            }

            if (Spit == null)
            {
                throw new NullReferenceException($"{nameof(Spit)} cannot be null for Weapon {this.InitSettings.SpitterName}");
            }

            if (SpitStrategy == null)
            {
                throw new NullReferenceException($"{nameof(SpitStrategy)} cannot be null for Weapon {this.InitSettings.SpitterName}");
            }

            CurrentSpitAmount = InitSettings.InitSpitAmount;

            SpitStrategy.SpitEvent += SpitEventHandler;
        }


        /// <summary>
        /// Start a Firing action spanned across multiple frames
        /// </summary>
        public virtual void StartSpitting()
        {
            SpitStrategy.StartSpitting();
        }

        /// <summary>
        /// Stop a firing action spanned across multiple frames
        /// </summary>
        public virtual void StopSpitting()
        {
            SpitStrategy.StopSpitting();
        }

        public void SpitEventHandler(Vector3 relPosition, Quaternion rotation)
        {
            if(CurrentSpitAmount <= 0)
            {
                SpitStrategy.StopSpitting();
                // Emit sound?
                return;
            }

            var spitGo = GameObject.Instantiate(this.Spit, this.transform.position + relPosition, rotation);
            spitGo.transform.parent = null;
            
            // Play sound
            if(SpitSound != null)
            {
                _staticObjects.Messenger.PublishPlaySound(this, null, SpitSound);
            }

            CurrentSpitAmount--;
        }
    }
}
