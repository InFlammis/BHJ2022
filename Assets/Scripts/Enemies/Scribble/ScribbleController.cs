using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.Levels;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using InFlammis.Victoria.Assets.Scripts.Player;
using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble
{
    public class ScribbleController : EnemyController
    {

        void Awake()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException("InitSettings");
            }

            Target = $"{this.GetType().Name}:{ GameObject.GetInstanceID()}";

            HealthManager = GameObject.GetComponentInChildren<HealthManager>();
            HealthManager.Target = Target;

            SubscribeToHealthManagerEvents();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            switch (col.gameObject.tag)
            {
                case "Player":
                {
                    this.HandleCollisionWithPlayer();
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

        public void HandleCollisionWithPlayer()
        {
            HealthManager.Kill();
        }

        public virtual void SubscribeToHealthManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManager_HasDied);
        }

        public virtual void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManager_HasDied);
        }

        void HealthManager_HasDied(object publisher, string target)
        {
            if (target != Target)
            {
                return;
            }

            StaticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.ExplodeSound);

            UnsubscribeToHealthManagerEvents();

            (StaticObjects.Messenger as IEnemyEventsPublisher).PublishHasDied(this, $"{target},{this.GameObject.GetInstanceID()}");
            StaticObjects.Messenger.PublishPlayerScored(this, $"{target},{this.GameObject.GetInstanceID()}", InitSettings.PlayerScoreWhenKilled);

            GameObject.Destroy(this.gameObject);
            ReleasePowerUp();
        }
    }
}
