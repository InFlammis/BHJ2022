using System;
using System.Collections;
using System.Linq;
using BulletHellJam2022.Assets.Scripts.Enemies.Infantry.StateMachine;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using BulletHellJam2022.Assets.Scripts.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Infantry
{
    /// <summary>
    /// Specialization of a IEnemyControllerCore for an Infantry enemy type
    /// </summary>
    public class InfantryControllerCore : 
        IEnemyControllerCore
    {
        public Messenger Messenger { get; set; }

        private string _Target;

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
        /// Collection of weapons available for the Enemy
        /// </summary>
        public WeaponBase[] Weapons { get; }

        /// <summary>
        /// Current weapon in use by the enemy
        /// </summary>
        public WeaponBase CurrentWeapon { get; set; }

        /// <summary>
        /// Current state the enemy is in
        /// </summary>
        public IInfantryState CurrentState { get; protected set; }

        /// <summary>
        /// Instance of the state factory
        /// </summary>
        private StateFactory _stateFactory;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="parent">The IEnemyControlle parent</param>
        /// <param name="healthManager">The healthManager instance</param>
        /// <param name="settings">The initial settings</param>
        public InfantryControllerCore(IEnemyController parent, IHealthManager healthManager, EnemySettings settings)
        {
            this.Messenger = GameObject.FindObjectOfType<Messenger>();
            Parent = parent;
            Transform = parent.GameObject.transform;
            Rigidbody = parent.GameObject.GetComponent<Rigidbody2D>();
            _Target = $"{this.Parent.GetType().Name}:{ parent.GameObject.GetInstanceID() }";

            HealthManager = healthManager;

            SubscribeToHealthManagerEvents();

            SubscribeToPlayerEvents();

            InitSettings = settings;
            Weapons = parent.Weapons.Select(x => x.GetComponent<WeaponBase>()).ToArray();
            CurrentWeapon = Weapons[0];

            _stateFactory = new StateFactory(this);
            CurrentState = _stateFactory.IdleState;
        }

        private void SubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.AddListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.AddListener(HealthManagerHealthLevelChanged);
        }

        private void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            messenger.HasDied.RemoveListener(HealthManagerHasDied);
            messenger.HealthLevelChanged.RemoveListener(HealthManagerHealthLevelChanged);
        }

        private void SubscribeToPlayerEvents()
        {
            var messenger = (Messenger as IPlayerEventsMessenger);

            messenger.HasDied.AddListener(PlayerHasDied);
        }

        private void UnsubscribeToPlayerEvents()
        {
            var messenger = (Messenger as IPlayerEventsMessenger);

            messenger.HasDied.RemoveListener(PlayerHasDied);
        }

        /// <summary>
        /// Manage collisions with the player
        /// </summary>
        public void HandleCollisionWithPlayer()
        {
            HealthManager.Kill();
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
        protected void ChangeState(IInfantryState newState)
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
        private void CurrentStateOnChangeState(IInfantryState state)
        {
            ChangeState(state);
        }

        void HealthManagerHasDied(object publisher, string target)
        {
            if (target != _Target)
            {
                return;
            }

            ChangeState(_stateFactory.IdleState);

            UnsubscribeToPlayerEvents();
            UnsubscribeToHealthManagerEvents();

            (Messenger as IEnemyEventsPublisher).PublishHasDied(this.Parent, $"Infantry,{this.Parent.GameObject.GetInstanceID()}");
            (Messenger as IEnemyEventsPublisher).PublishPlayerScored(this.Parent, $"Infantry,{this.Parent.GameObject.GetInstanceID()}", InitSettings.PlayerScoreWhenKilled);
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
