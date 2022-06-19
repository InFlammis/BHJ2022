using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.Levels;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using InFlammis.Victoria.Assets.Scripts.Player;
using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle
{
    public class TriangleController : EnemyController
    {
        #region Unity methods

        void Awake()
        {
            Target = $"{this.GetType().Name}:{ GameObject.GetInstanceID()}";

            HealthManager = GameObject.GetComponentInChildren<HealthManager>();
            HealthManager.Target = Target;

            SubscribeToHealthManagerEvents();

            Core = new TriangleControllerCore(this, HealthManager, InitSettings);
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
