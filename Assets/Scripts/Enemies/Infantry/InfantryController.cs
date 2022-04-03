using System;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.Managers.Levels;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using BulletHellJam2022.Assets.Scripts.Weapons;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Infantry
{
    /// <summary>
    /// Specialization of a EnemyController for an Infantry enemy type
    /// </summary>
    public class InfantryController : 
        EnemyController
    {
        #region Unity methods

        void Awake()
        {
            Target = $"{this.GetType().Name}:{ GameObject.GetInstanceID()}";

            HealthManager = GameObject.GetComponentInChildren<HealthManager>();
            HealthManager.Target = Target;

            SubscribeToHealthManagerEvents();

            CheckWeaponsConfiguration();

            Core = new InfantryControllerCore(this, HealthManager, InitSettings);

        }

        public virtual void SubscribeToHealthManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManagerHasDied);
        }

        public virtual void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManagerHasDied);
        }


        void Start()
        {
            var sceneManagerGO = GameObject.FindGameObjectWithTag("SceneManager");
            var sceneManager = sceneManagerGO?.GetComponent<LevelManager>();

            if(sceneManager == null)
            {
                Debug.LogError("SceneManager not found");
            }

            //_SoundManager = gameObject.GetComponent<EnemySoundManager>();

            //if(_SoundManager == null)
            //{
            //    Debug.LogError("SoundManager not found");
            //}

            //_SoundManager.SceneManager = sceneManager;


            if (InitSettings == null)
            {
                throw new NullReferenceException("InitSettings");
            }

            var player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.Log("Player object not found");
            }
            else
            {
                Core.PlayerControllerCore = player.GetComponent<PlayerController>().Core;
            }

            Core.OnStart();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            //Debug.Log($"Collision detected with {col.gameObject.name}");

            switch (col.gameObject.tag)
            {
                case "Player":
                {
                    Core.HandleCollisionWithPlayer();
                    break;
                }
                case "Bullet":
                {
                    //The collision is managed by the bullet
                    StaticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.HitSound);

                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            Core?.Move();
        }

        #endregion

        /// <summary>
        /// Checks that the weapon configuration is available for each weapon
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckWeaponsConfiguration()
        {
            Weapons = this.GameObject.GetComponentsInChildren<WeaponBase>();
            foreach (var weapon in Weapons)
            {
                //If the current weapon has no configuration, throw.
                if (weapon.InitSettings == null)
                {
                    throw new Exception($"No settings for weapon {weapon.WeaponType}");
                }
            }
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if (target != Target)
            {
                return;
            }

            Debug.Log($"Destroying object {this.gameObject.name}");

            StaticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.ExplodeSound);

            UnsubscribeToHealthManagerEvents();

            var eeInstance = Instantiate(this.ExplosionEffect, this.gameObject.transform);
            eeInstance.transform.SetParent(null);

            Destroy(eeInstance, 4);

            GameObject.Destroy(this.gameObject);
            ReleasePowerUp();
        }
    }
}
