using System;
using BulletHellJam2022.Assets.Scripts.Enemies.Pawn.StateMachine;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn
{
    /// <summary>
    /// Specialization of a IEnemyControllerCore for a Pawn enemy type
    /// </summary>
    public class PawnControllerCore : 
        IEnemyControllerCore
    {
        private IMessenger _messenger => Parent.StaticObjects.Messenger;

        //private string _Target;

        /// <inheritdoc/>
        public IPlayerControllerCore PlayerControllerCore { get; set; }
        /// <inheritdoc/>
        public IEnemyController Parent { get; protected set; }
        /// <inheritdoc/>
        public Transform Transform { get; protected set; }
        /// <inheritdoc/>
        public Rigidbody2D Rigidbody { get; protected set; }
        /// <inheritdoc/>
        public EnemySettings InitSettings { get; protected set; }
        /// <inheritdoc/>
        public IHealthManager HealthManager { get; }

        /// <summary>
        /// Current state the enemy is in
        /// </summary>
        public IPawnState CurrentState { get; protected set; }

        /// <summary>
        /// Instance of the state factory
        /// </summary>
        private StateFactory _stateFactory;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="parent">The IEnemyController parent</param>
        /// <param name="healthManager">The healthManager instance</param>
        /// <param name="settings">The initial settings</param>
        public PawnControllerCore(IEnemyController parent, IHealthManager healthManager, EnemySettings settings)
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

        /// <summary>
        /// Invoked on Start by the parent MonoBehaviour
        /// </summary>
        public void OnStart()
        {
            if(PlayerControllerCore != null)
            {
                ChangeState(_stateFactory.SeekState);
            }
            else
            {
                ChangeState(_stateFactory.IdleState);
            }

        }

        /// <summary>
        /// EventHandler for the Change state invokation from the current state
        /// </summary>
        /// <param name="newState">The new state to enable</param>
        protected void ChangeState(IPawnState newState)
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

        /// <summary>
        /// EventHandler for the Change state invokation from the current state
        /// </summary>
        /// <param name="newState">The new state to enable</param>
        private void CurrentStateOnChangeState(IPawnState state)
        {
            ChangeState(state);
        }

        /// <summary>
        /// Move the enemy
        /// </summary>
        public void Move()
        {
            CurrentState.Move();
            CurrentState.Rotate();
        }

        /// <summary>
        /// Manage collisions with the player
        /// </summary>
        public void HandleCollisionWithPlayer()
        {
            HealthManager.Kill();
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if(target != Parent.Target)
            {
                return;
            }
            ChangeState(_stateFactory.IdleState);

            UnsubscribeToPlayerEvents();
            UnsubscribeToHealthManagerEvents();

            (_messenger as IEnemyEventsPublisher).PublishHasDied(this.Parent, $"Pawn,{this.Parent.GameObject.GetInstanceID()}");
            (_messenger as IEnemyEventsPublisher).PublishPlayerScored(this.Parent, $"Pawn,{this.Parent.GameObject.GetInstanceID()}", InitSettings.PlayerScoreWhenKilled);
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
