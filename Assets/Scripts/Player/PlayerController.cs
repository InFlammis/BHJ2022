using System;
using System.Collections.Generic;
using System.Linq;
using BulletHellJam2022.Assets.Scripts.Enemies;
using BulletHellJam2022.Assets.Scripts.Managers;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.Managers.Levels;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace BulletHellJam2022.Assets.Scripts.Player
{
    /// <summary>
    /// Controller for the player.
    /// Implements the Core pattern. The core logic of the Controller is actually contained in the Core instance.
    /// </summary>
    public class PlayerController : 
        MyMonoBehaviour, 
        IPlayerController
    {
        //[SerializeField] private Messenger _messenger;

        //public IMessenger Messenger => _messenger;
        [SerializeField] private StaticObjectsSO _staticObjects;

        /// <summary>
        /// Reference to the SoundManager instance
        /// </summary>
        protected PlayerSoundManager _SoundManager;

        /// <inheritdoc/>
        public IPlayerControllerCore Core { get; set; }

        /// <inheritdoc/>
        public IHealthManager HealthManager { get; protected set; }

        /// <inheritdoc/>
        public PlayerSettings InitSettings => initSettings;

        /// <inheritdoc/>
        public WeaponBase[] Weapons { get; protected set; }


        private readonly string _Target = "Player";

        [SerializeField]
        private GameObject ExplosionEffect;

        [SerializeField]
        private PlayerSettings initSettings;

        /// <inheritdoc/>
        /// For testing <see cref="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html"/>
        /// <param name="context"></param>
        public void OnMove(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();

            if (context.performed)
            {
                Core.SetPlayerInput(inputVector);
                Debug.Log($"Moving {inputVector}");

            }
            else if (context.canceled)
            {
                Debug.Log("Not moving");
                Core.SetPlayerInput(inputVector);
            }
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();

            if (context.performed)
            {
                Core.Rotate(inputVector);
                Debug.Log($"Rotating {inputVector}");

            }
            else if (context.canceled)
            {
                Debug.Log("Not rotating");
                Core.Rotate(inputVector);
            }

        }

        /// <inheritdoc/>
        /// For testing <see cref="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html"/>
        /// <param name="context"></param>
        public void OnFire(InputAction.CallbackContext context)
        {
            
            if (context.started)
            {
                //Debug.Log("OnFire started");
                Core.StartFiring();
            }
            else if (context.canceled)
            {
                //Debug.Log("OnFire canceled");
                Core.StopFiring();
            }
        }

        /// <inheritdoc/>
        /// For testing <see cref="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html"/>
        /// <param name="context"></param>
        public void OnFireAlt(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                //Debug.Log("OnFireAlt started");
                //Do an action
                Core.FireAlt();
            }
            else if (context.canceled)
            {
                //Debug.Log("OnFireAlt canceled");
            }
        }

        /// <inheritdoc/>
        /// For testing <see cref="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html"/>
        /// <param name="context"></param>
        public void OnOpenSelectionMenu(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                //Debug.Log("OnOpenSelectionMenu started");
                //Do an action
                Core.OpenSelectionMenu();
            }
            else if (context.canceled)
            {
                //Debug.Log("OnOpenSelectionMenu canceled");
            }
        }

        /// <inheritdoc/>
        public void OnTurnLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnLeft();
                //Debug.Log($"Turning {Core.PlayerInput}");
            }
            else if (context.canceled)
            {
            }
        }

        /// <inheritdoc/>
        public void OnTurnRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnRight();
                //Debug.Log($"Turning {Core.PlayerInput}");
            }
            else if (context.canceled)
            {
            }
        }

        /// <inheritdoc/>
        public void OnTurnUp(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnUp();
                //Debug.Log($"Turning {Core.PlayerInput}");
            }
            else if (context.canceled)
            {
            }
        }

        /// <inheritdoc/>
        public void OnTurnDown(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnDown();
                //Debug.Log($"Turning {Core.PlayerInput}");
            }
            else if (context.canceled)
            {
            }
        }

        #region Unity methods

        private InputAction leftMouseClick;

        private void LeftMouseClicked()
        {
            print("LeftMouseClicked");
        }
        void Awake()
        {
            leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
            leftMouseClick.performed += ctx => LeftMouseClicked();
            leftMouseClick.Enable();
            //HealthManager = new HealthManager(_Target, initSettings.InitHealth, initSettings.InitHealth, false);
            HealthManager = GameObject.GetComponentInChildren<HealthManager>();

            SubscribeToHealthManagerEvents();
            CheckWeaponsConfiguration();

            Core = new PlayerControllerCore(this);
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


        void Start()
        {
            if (initSettings == null)
            {
                throw new NullReferenceException("InitSettings");
            }

            var sceneManagerGO = GameObject.FindGameObjectWithTag("SceneManager");
            var sceneManager = sceneManagerGO?.GetComponent<LevelManager>();

            if (sceneManager == null)
            {
                Debug.LogError("SceneManager not found");
            }

            _SoundManager = gameObject.GetComponent<PlayerSoundManager>();

            if (_SoundManager == null)
            {
                Debug.LogError("SoundManager not found");
            }

            _SoundManager.SceneManager = sceneManager;


        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                var enemyController = col.gameObject.GetComponent<EnemyController>();
                Core.HandleCollisionWithEnemy(enemyController.Core);
            }
            else if(col.gameObject.tag == "EnemyBullet")
            {
                _SoundManager.PlayHitSound();
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "PowerUp")
            {
                _SoundManager.PlayPowerUpSound();
            }
        }

        void FixedUpdate()
        {
            Core.Move();
        }

        #endregion

        /// <summary>
        /// Check that all available weapons are properly configured
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckWeaponsConfiguration()
        {
            Weapons = this.GameObject.GetComponentsInChildren<WeaponBase>();
            foreach (var weapon in Weapons)
            {
                //Check if there is a new settings for the current weapon. If there is, assign.
                var weaponSettings =
                    initSettings.WeaponSettings.SingleOrDefault(x => x.WeaponType == weapon.WeaponType);
                if (weaponSettings != null)
                {
                    weapon.InitSettings = weaponSettings;
                }

                //If the current weapon has no configuration, throw.
                if (weapon.InitSettings == null)
                {
                    throw new Exception($"No settings for weapon {weapon.WeaponType}");
                }
            }
        }

        /// <summary>
        /// EventHandler for a HealthLevelChanged event raised by the HealthManager
        /// </summary>
        /// <param name="value">New health level</param>
        /// <param name="maxValue">Max health level</param>

        void HealthManagerHasDied(object publisher, string target)
        {
            if(target != null && target != _Target)
            {
                return;
            }

            (_staticObjects.Messenger as IPlayerEventsPublisher).PublishHasDied(this, null);

            _SoundManager.PlayExplodeSound();

            UnsubscribeToHealthManagerEvents();

            var eeInstance = Instantiate(this.ExplosionEffect, this.gameObject.transform);
            eeInstance.transform.SetParent(null);

            Destroy(eeInstance, 4);

            Destroy(this.gameObject);
        }

        public void HealthManagerHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            if (target != null && target != _Target)
            {
                return;
            }

            (_staticObjects.Messenger as IPlayerEventsPublisher).PublishHealthLevelChanged(publisher, target, healthLevel, maxHealthLevel);
        }

        public void HealthChargerCollected(object publisher, string target, int health)
        {
            if (target != null && target != _Target)
            {
                return;
            }

            this.HealthManager.Heal(health);

        }
    }
}
