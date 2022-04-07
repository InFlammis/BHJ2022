﻿using BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    public class Level_01ManagerCore : 
        ILevelManagerCore
    {
        public IPlayerControllerCore PlayerControllerCore { get; set; }

        public State CurrentState { get; private set; }

        public ILevelManager LevelManager { get; set; }

        public bool SpawnEnemiesEnabled = true;

        protected PlayerInput _playerInput;

        private StateConfiguration _stateConfiguration;

        /// <summary>
        /// Create and instance of the class
        /// </summary>
        /// <param name="levelManager">Reference to the <see cref="ILevelManager"/> instance</param>
        public Level_01ManagerCore(ILevelManager levelManager)
        {
            LevelManager = levelManager;
        }

        public void OnStart()
        {
            this.PlayerControllerCore = LevelManager.PlayerControllerCore;

            StartGame();
        }

        private void StartGame()
        {
            this.PlayerControllerCore.HealthManager.Heal();

            _stateConfiguration = new StateConfiguration(
                messenger: LevelManager.StaticObjects.Messenger,
                levelManagerCore: this,
                spawnEnemiesEnabled: true
            );

            ChangeStateRequestEventHandler(this, null, new WaitForStart(_stateConfiguration));
        }

        /// <summary>
        /// Handler for a request to change current state.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="e">The new state.</param>
        protected void ChangeStateRequestEventHandler(object publisher, string target, State e)
        {
            if (CurrentState != null)
            {
                CurrentState.OnExit();
            }
            CurrentState = e;
            CurrentState.OnEnter();
        }

        public void OnAwake() 
        {
            _playerInput = LevelManager.GameObject.GetComponent<PlayerInput>();
            LevelManager.StaticObjects.Messenger.ChangeStateRequest.AddListener(ChangeStateRequestEventHandler);

        }


        public void Move(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {

                case InputActionPhase.Started:
                    //Debug.Log($"{context.action} Started");
                    break;
                case InputActionPhase.Performed:
                    //Debug.Log($"{context.action} Performed");
                    break;
                case InputActionPhase.Canceled:
                    //Debug.Log($"{context.action} Cancelled");
                    break;
            }
        }

        public void DisablePlayerInput()
        {
            _playerInput.enabled = false;
        }

        public void EnablePlayerInput()
        {
            _playerInput.enabled = true;
        }

        private void GameOver()
        {
            LevelManager.StaticObjects.Messenger.PublishGameOver(this, null);
            ChangeStateRequestEventHandler(this, null, new StateMachine.GameOver(_stateConfiguration));

        }
        public void PlayerHasDied(object publisher, string target)
        {
            GameOver();
        }

        public void OrchestrationManagerOrchestrationComplete(object publisher, string target)
        {
            ChangeStateRequestEventHandler(this, null, new Win(_stateConfiguration));
        }
    }
}
