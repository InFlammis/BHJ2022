using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using System;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public class Spitter :  MonoBehaviour
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
        public SpitterSettings SpitterInitSettings;

        public SpitSettings SpitInitSettings;

        [Header("Sounds", order = 4)]
        [SerializeField] 
        private Sound SpitSound;

        [Header("Runtime values", order = 4)]
        /// <summary>
        /// Current amount of spits
        /// </summary>
        public int CurrentSpitAmount;

        private SpitStrategy SpitStrategy;

        private float lastSpitTime;



        void Awake()
        {
            if (this.SpitterInitSettings == null)
            {
                throw new NullReferenceException($"{nameof(SpitterInitSettings)} cannot be null for Spitter.");
            }

            if (this.SpitInitSettings == null)
            {
                throw new NullReferenceException($"{nameof(SpitInitSettings)} cannot be null for Spitter.");
            }

            if (this.Spit == null)
            {
                throw new NullReferenceException($"{nameof(Spit)} cannot be null for Weapon {this.SpitterInitSettings.SpitterName}");
            }

            //this.Spit.GetComponent<SpitBase>().InitSettings = this.SpitInitSettings;

            this.SpitStrategy = GetComponentInChildren<SpitStrategy>();
            if (this.SpitStrategy == null)
            {
                throw new NullReferenceException($"{nameof(SpitStrategy)} cannot be null for Weapon {this.SpitterInitSettings.SpitterName}");
            }

            CurrentSpitAmount = SpitterInitSettings.InitSpitAmount;

            // So the spitter can shoot immediately at start.
            lastSpitTime = -SpitterInitSettings.InterSpitInterval;

            SpitStrategy.SpitEvent += SpitEventHandler;
            SpitStrategy.EndSpitEvent += SpitStrategy_EndSpitEvent;
        }

        private void SpitStrategy_EndSpitEvent(float time)
        {
            lastSpitTime = time;
        }

        /// <summary>
        /// Start a Firing action spanned across multiple frames
        /// </summary>
        public virtual void StartSpitting()
        {
            if(Time.time - SpitterInitSettings.InterSpitInterval < lastSpitTime)
            {
                return;
            }

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
            var spitGo = GameObject.Instantiate(this.Spit, transform.position + relPosition, transform.rotation * rotation);
            spitGo.GetComponent<SpitBase>().InitSettings = SpitInitSettings;

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
