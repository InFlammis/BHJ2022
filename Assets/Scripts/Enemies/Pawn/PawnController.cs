﻿using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.Managers.Levels;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn
{
    /// <summary>
    /// Specialization of a EnemyController for a Pawn enemy type
    /// </summary>
    public class PawnController : 
        EnemyController
    {
        #region Unity methods

        void Awake()
        {
            Target = $"{this.GetType().Name}:{ GameObject.GetInstanceID()}";

            HealthManager = GameObject.GetComponentInChildren<HealthManager>();
            HealthManager.Target = Target;

            SubscribeToHealthManagerEvents();

            Core = new PawnControllerCore(this, HealthManager, InitSettings);
        }

        void Start()
        {
            var sceneManagerGO = GameObject.FindGameObjectWithTag("SceneManager");
            var sceneManager = sceneManagerGO?.GetComponent<LevelManager>();

            if (sceneManager == null)
            {
                Debug.LogError("SceneManager not found");
            }

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


        public virtual void SubscribeToHealthManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.AddListener(HealthManagerHealthLevelChanged);
        }

        public virtual void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.RemoveListener(HealthManagerHealthLevelChanged);
        }

        void OnCollisionEnter2D(Collision2D col)
        {

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
            Core.Move();
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if (target != Target)
            {
                return;
            }

            StaticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.ExplodeSound);

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
