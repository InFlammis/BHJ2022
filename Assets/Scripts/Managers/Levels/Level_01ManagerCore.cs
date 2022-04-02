using BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    public class Level_01ManagerCore : 
        ILevelManagerCore
    {
        /// <inheritdoc/>
        public IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <inheritdoc/>
        public State CurrentState { get; private set; }
        /// <inheritdoc/>
        public ILevelManager LevelManager { get; set; }

        public IMessenger Messenger => LevelManager.Messenger;

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

        /// <inheritdoc/>
        public void OnStart()
        {
            this.PlayerControllerCore = LevelManager.PlayerControllerCore;

            StartGame();
        }

        private void StartGame()
        {
            this.PlayerControllerCore.HealthManager.Heal();

            _stateConfiguration = new StateConfiguration(
                messenger: Messenger,
                levelManagerCore: this,
                spawnEnemiesEnabled: true
            );

            ChangeStateRequestEventHandler(this, new WaitForStart(_stateConfiguration));
        }

        /// <summary>
        /// Handler for a request to change current state.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="e">The new state.</param>
        protected void ChangeStateRequestEventHandler(object sender, State e)
        {
            //Debug.Log($"Changing state from {sender} to {e}");
            if (CurrentState != null)
            {
                CurrentState.ChangeStateRequestEvent -= ChangeStateRequestEventHandler;
                CurrentState.OnExit();
            }
            CurrentState = e;
            CurrentState.ChangeStateRequestEvent += ChangeStateRequestEventHandler;
            CurrentState.OnEnter();
        }

        /// <inheritdoc/>
        public void OnAwake() 
        {
            _playerInput = LevelManager.GameObject.GetComponent<PlayerInput>();
        }


        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void DisablePlayerInput()
        {
            _playerInput.enabled = false;
        }

        /// <inheritdoc/>
        public void EnablePlayerInput()
        {
            _playerInput.enabled = true;
        }

        private void GameOver()
        {
            Messenger.PublishGameOver(this, null);
            ChangeStateRequestEventHandler(this, new StateMachine.GameOver(_stateConfiguration));

        }
        public void PlayerHasDied(object publisher, string target)
        {
            GameOver();
        }

        public void OrchestrationManagerOrchestrationComplete(object publisher, string target)
        {
            ChangeStateRequestEventHandler(this, new Win(_stateConfiguration));
        }
    }
}
