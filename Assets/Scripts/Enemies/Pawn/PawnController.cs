using System;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.Managers.Levels;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn
{
    /// <summary>
    /// Specialization of a EnemyController for a Pawn enemy type
    /// </summary>
    public class PawnController : 
        EnemyController
    {
        public Messenger Messenger { get; set; }

        private string _Target;

        #region Unity methods

        void Awake()
        {
            Messenger = GameObject.FindObjectOfType<Messenger>();
            _Target = $"{this.GetType().Name}:{ GameObject.GetInstanceID()}";

            HealthManager = new HealthManager(_Target, InitSettings.InitHealth, InitSettings.InitHealth, false);

            SubscribeToHealthManagerEvents();

            Core = new PawnControllerCore(this, HealthManager, InitSettings);
        }

        public virtual void SubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.AddListener(HealthManagerHealthLevelChanged);
        }

        public virtual void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.RemoveListener(HealthManagerHealthLevelChanged);
        }


        void Start()
        {
            var sceneManagerGO = GameObject.FindGameObjectWithTag("SceneManager");
            var sceneManager = sceneManagerGO?.GetComponent<LevelManager>();

            if (sceneManager == null)
            {
                Debug.LogError("SceneManager not found");
            }

            _SoundManager = gameObject.GetComponent<EnemySoundManager>();

            if (_SoundManager == null)
            {
                Debug.LogError("SoundManager not found");
            }

            _SoundManager.SceneManager = sceneManager;

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
                    _SoundManager.PlayHitSound();
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            Core.Move();
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if (target != _Target)
            {
                return;
            }

            //Debug.Log($"Destroying object {this.gameObject.name}");

            _SoundManager.PlayExplodeSound();

            UnsubscribeToHealthManagerEvents();

            var eeInstance = Instantiate(this.ExplosionEffect, this.gameObject.transform);
            eeInstance.transform.SetParent(null);

            GameObject.Destroy(this.gameObject);
            ReleasePowerUp();
        }

        void HealthManagerHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }

        #endregion
    }
}
