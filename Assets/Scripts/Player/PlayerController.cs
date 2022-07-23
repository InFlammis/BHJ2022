using InFlammis.Victoria.Assets.Scripts.Enemies;
using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.Levels;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using InFlammis.Victoria.Assets.Scripts.Weapons;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InFlammis.Victoria.Assets.Scripts.Player
{
    public class PlayerController : 
        MyMonoBehaviour, 
        IPlayerController
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        [SerializeField] private PlayerSoundSettingsSO _soundSettings;

        public IPlayerControllerCore Core { get; set; }

        public IHealthManager HealthManager { get; protected set; }

        public PlayerSettings InitSettings => initSettings;

        public Spitter[] Weapons { get; protected set; }


        private readonly string _Target = "Player";
        private Camera mainCamera;
        private PlayerActionAsset playerActionAsset;
        private PlayerInput playerInput;

        [SerializeField]
        private Vector2 inputMovement;

        [SerializeField]
        private Vector2 inputRotation;

        [SerializeField]
        private bool isGamepad;

        [SerializeField]
        private GameObject ExplosionEffect;

        [SerializeField]
        private PlayerSettings initSettings;

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMovement = context.ReadValue<Vector2>();

            if (context.performed)
            {
                Core.SetPlayerMovement(inputMovement);
                //Debug.Log($"Moving {inputMovement}");
            }
            else if (context.canceled)
            {
                //Debug.Log("Not moving");
                Core.SetPlayerMovement(inputMovement);
            }
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if(isGamepad)
                {
                    inputRotation = context.ReadValue<Vector2>();
                    Core.SetPlayerRotation(inputRotation, isGamepad);
                }
                else
                {
                    //Debug.Log("InputPosition: " + context.ReadValue<Vector2>());
                    //inputRotation = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
                    //Debug.Log("Input World position: " + inputRotation);
                    inputRotation = context.ReadValue<Vector2>();
                    Core.SetPlayerRotation(inputRotation, isGamepad);
                    //Debug.Log($"Rotating {inputRotation}");
                }
            }
            /*else if (context.canceled)
            {
                inputRotation = Vector2.zero;

                Core.SetPlayerRotation(inputRotation);
            }*/
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Core.StartFiring();
            }
            else if (context.canceled)
            {
                Core.StopFiring();
            }
        }

        public void OnFireAlt(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                //Do an action
                Core.FireAlt();
            }
            else if (context.canceled)
            {
            }
        }

        public void OnOpenSelectionMenu(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                //Do an action
                Core.OpenSelectionMenu();
            }
            else if (context.canceled)
            {
            }
        }

        public void OnTurnLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnLeft();
            }
            else if (context.canceled)
            {
            }
        }

        public void OnTurnRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnRight();
            }
            else if (context.canceled)
            {
            }
        }

        public void OnTurnUp(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnUp();
            }
            else if (context.canceled)
            {
            }
        }

        public void OnTurnDown(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Core.TurnDown();
            }
            else if (context.canceled)
            {
            }
        }
        
        public void OnDeviceChange(PlayerInput playerInput)
        {
            isGamepad = playerInput.currentControlScheme.Equals("Gamepad") ? true : false;
        }

        #region Unity methods

        //private InputAction leftMouseClick;

        /*private void LeftMouseClicked()
        {
            print("LeftMouseClicked");
        }*/

        void Awake()
        {
            mainCamera = Camera.main;
            playerActionAsset = new PlayerActionAsset();
            playerInput = GetComponent<PlayerInput>();

            //leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
            //leftMouseClick.performed += ctx => LeftMouseClicked();
            //leftMouseClick.Enable();
            HealthManager = GameObject.GetComponentInChildren<HealthManager>();

            SubscribeToHealthManagerEvents();
            SubscribeToRequestsForPlayer();
            CheckWeaponsConfiguration();

            Core = new PlayerControllerCore(this);
        }

        void OnEnable()
        {
            playerActionAsset.Enable();
        }

        void OnDisable()
        {
            playerActionAsset.Disable();
        }

        private void SubscribeToRequestsForPlayer()
        {
            _staticObjects.Messenger.RequestForPlayerTransformEvent += Messenger_RequestForPlayerTransformEvent;
            _staticObjects.Messenger.RequestForPlayerIsAliveEvent += Messenger_RequestForPlayerIsAliveEvent;
        }

        private void UnSubscribeToRequestsForPlayer()
        {
            _staticObjects.Messenger.RequestForPlayerTransformEvent -= Messenger_RequestForPlayerTransformEvent;
            _staticObjects.Messenger.RequestForPlayerIsAliveEvent -= Messenger_RequestForPlayerIsAliveEvent;
        }

        private bool Messenger_RequestForPlayerIsAliveEvent(object publisher, string target)
        {
            return true;
        }

        private Transform Messenger_RequestForPlayerTransformEvent(object publisher, string target)
        {
            return this.gameObject.transform;
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
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                var enemyController = col.gameObject.GetComponent<EnemyController>();
                Core.HandleCollisionWithEnemy(enemyController);
            }
            else if(col.gameObject.tag == "EnemyBullet")
            {
                _staticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.HitSound);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "PowerUp")
            {
                _staticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.PowerUpSound);
            }
        }

        void FixedUpdate()
        {
            // Player Movement
            Core.Move();

            // Player Rotation/Aim
            Core.Rotate(mainCamera.ScreenToWorldPoint(playerActionAsset.Player.Rotate.ReadValue<Vector2>()), isGamepad);
        }
        #endregion

        /// <summary>
        /// Check that all available weapons are properly configured
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckWeaponsConfiguration()
        {
            Weapons = this.GameObject.GetComponentsInChildren<Spitter>();
            foreach (var weapon in Weapons)
            {
                ////Check if there is a new settings for the current weapon. If there is, assign.
                //var weaponSettings =
                //    initSettings.WeaponSettings.SingleOrDefault(x => x.WeaponType == weapon.WeaponType);
                //if (weaponSettings != null)
                //{
                //    weapon.InitSettings = weaponSettings;
                //}

                ////If the current weapon has no configuration, throw.
                //if (weapon.InitSettings == null)
                //{
                //    throw new Exception($"No settings for weapon {weapon.WeaponType}");
                //}
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

            _staticObjects.Messenger.PublishPlaySound(this, null, _soundSettings.ExplodeSound);

            UnsubscribeToHealthManagerEvents();

            UnSubscribeToRequestsForPlayer();

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
