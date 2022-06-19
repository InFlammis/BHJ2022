using InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.MessageBroker;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using InFlammis.Victoria.Assets.Scripts.Player;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble
{
    public partial class ScribbleControllerCore : IEnemyControllerCore
    {
        private IMessenger _messenger => Parent.StaticObjects.Messenger;

        public IEnemyController Parent { get; protected set; }

        public Transform Transform { get; protected set; }

        public Rigidbody2D Rigidbody { get; protected set; }

        public EnemySettings InitSettings { get; protected set; }

        public IHealthManager HealthManager { get; }

        public IScribbleState CurrentState { get; protected set; }

        private StateFactory _stateFactory;

        public ScribbleControllerCore(IEnemyController parent, IHealthManager healthManager, EnemySettings settings)
        {
            Parent = parent;
            Transform = parent.GameObject.transform;
            Rigidbody = parent.GameObject.GetComponent<Rigidbody2D>();

            HealthManager = healthManager;

            SubscribeToHealthManagerEvents();

            SubscribeToPlayerEvents();

            InitSettings = settings;

            _stateFactory = new StateFactory(this);
            CurrentState = _stateFactory.IdleState;
        }

        private void SubscribeToHealthManagerEvents()
        {
            var messenger = (_messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.AddListener(HealthManagerHealthLevelChanged);
        }

        private void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (_messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.RemoveListener(HealthManagerHealthLevelChanged);
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
            CurrentState.Move();
            CurrentState.Rotate();
        }

        public void OnStart()
        {
            if (_messenger.RequestForPlayerIsAlive(this, null))
            {
                ChangeState(_stateFactory.SeekState);
            }
            else
            {
                ChangeState(_stateFactory.IdleState);
            }
        }

        protected void ChangeState(IScribbleState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.OnExit();
                CurrentState.ChangeState -= CurrentStateOnChangeState;
            }

            if (CurrentState == newState)
            {
                return;
            }

            CurrentState = newState;
            CurrentState.ChangeState += CurrentStateOnChangeState;
            CurrentState.OnEnter();
        }
        private void CurrentStateOnChangeState(IScribbleState state)
        {
            ChangeState(state);
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if (target != Parent.Target)
            {
                return;
            }
            ChangeState(_stateFactory.IdleState);

            UnsubscribeToPlayerEvents();
            UnsubscribeToHealthManagerEvents();

            (_messenger as IEnemyEventsPublisher).PublishHasDied(this.Parent, $"{target},{this.Parent.GameObject.GetInstanceID()}");
            _messenger.PublishPlayerScored(this.Parent, $"{target},{this.Parent.GameObject.GetInstanceID()}", InitSettings.PlayerScoreWhenKilled);
        }

        void HealthManagerHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }

        void PlayerHasDied(object publisher, string target)
        {
            ChangeState(_stateFactory.IdleState);
        }
    }
}
