using InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.MessageBroker;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using InFlammis.Victoria.Assets.Scripts.Player;
using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble
{
    [Obsolete]
    public partial class ScribbleControllerCore : IEnemyControllerCore
    {
        private IMessenger _messenger => Parent.StaticObjects.Messenger;

        public IEnemyController Parent { get; protected set; }

        public Transform Transform { get; protected set; }

        public Rigidbody2D Rigidbody { get; protected set; }

        public EnemySettings InitSettings { get; protected set; }

        public IHealthManager HealthManager { get; }

        public ScribbleControllerCore(IEnemyController parent, IHealthManager healthManager, EnemySettings settings)
        {
            Parent = parent;
            Transform = parent.GameObject.transform;
            Rigidbody = parent.GameObject.GetComponent<Rigidbody2D>();

            HealthManager = healthManager;

            SubscribeToHealthManagerEvents();

            SubscribeToPlayerEvents();

            InitSettings = settings;
        }

        private void SubscribeToHealthManagerEvents()
        {
            var messenger = (_messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManagerHasDied);
        }

        private void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (_messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManagerHasDied);
        }

        private void SubscribeToPlayerEvents()
        {
            var messenger = (_messenger as IPlayerEventsMessenger);

            messenger.HasDied.AddListener(PlayerHasDied);
        }

        private void UnsubscribeToPlayerEvents()
        {
            var messenger = (_messenger as IPlayerEventsMessenger);

            messenger.HasDied.RemoveListener(PlayerHasDied);
        }

        public void HandleCollisionWithPlayer()
        {
            HealthManager.Kill();
        }

        public void Move()
        {
        }

        public void OnStart()
        {
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if (target != Parent.Target)
            {
                return;
            }

            UnsubscribeToPlayerEvents();
            UnsubscribeToHealthManagerEvents();
        }

        void PlayerHasDied(object publisher, string target)
        {
        }
    }
}
