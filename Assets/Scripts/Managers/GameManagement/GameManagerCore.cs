using System;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine;
using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement
{
    /// <summary>
    /// Core for the GameManager class. Contains the logic that rules the GameManager behaviour.
    /// </summary>
    public class GameManagerCore : IGameManager
    {
        /// <summary>
        /// Reference to the Parent instance
        /// </summary>
        public readonly IMyMonoBehaviour Parent;

        private IUnitySceneManagerWrapper _sceneManagerWrapper;

        /// <summary>
        /// Stack for the State machine
        /// </summary>
        protected StateStack _stateStack = new StateStack();

        /// <inheritdoc/>
        public ISoundManager SoundManager { get; protected set; }

        public StaticObjectsSO StaticObjects => (Parent as IGameManager).StaticObjects;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="parent">Reference to the parent object</param>
        public GameManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
            //SoundManager = StaticObjects.SoundManager;
            _sceneManagerWrapper = UnitySceneManagerWrapper.Instance;
        }

        #region MonoBehaviour methods

        public void OnAwake()
        {
            _stateStack.PoppingStateEvent += StateStack_PoppingStateEvent;
            _stateStack.PushingStateEvent += StateStack_PushingStateEvent;

            StaticObjects.Messenger.PauseGame.AddListener(PauseGameEventHandler);
            StaticObjects.Messenger.StartGame.AddListener(StartGameEventHandler);
            StaticObjects.Messenger.ResumeGame.AddListener(ResumeGameEventHandler);
            StaticObjects.Messenger.QuitCurrentGame.AddListener(QuitCurrentGameEventHandler);
            StaticObjects.Messenger.QuitGame.AddListener(QuitGameEventHandler);
            StaticObjects.Messenger.OpenCredits.AddListener(OpenCreditsEventHandler);
            StaticObjects.Messenger.OpenHelp.AddListener(OpenHelpEventHandler);
            StaticObjects.Messenger.BackToMain.AddListener(BackToMainEventHandler);

        }

        public void OnStart()
        {
            PushState(new Init(this, _sceneManagerWrapper));
        }

        #endregion

        #region Event Handlers for StateStack events

        /// <summary>
        /// Attach the eventhandlers to the current state
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="state">New state being pushed into the stack</param>
        protected virtual void StateStack_PushingStateEvent(object sender, State state)
        {
        }

        /// <summary>
        /// Detache the eventhandlers from the state being popped out of the stack
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="state">The state being popped out of the stack</param>
        protected virtual void StateStack_PoppingStateEvent(object sender, State state)
        {
        }

        private void BackToMainEventHandler(object arg0, string arg1)
        {
            ReplaceState(new Init(this, _sceneManagerWrapper));
        }

        private void OpenHelpEventHandler(object arg0, string arg1)
        {
            ReplaceState(new Help(this, _sceneManagerWrapper));
        }

        private void OpenCreditsEventHandler(object arg0, string arg1)
        {
            ReplaceState(new Credits(this, _sceneManagerWrapper));
        }

        private void QuitGameEventHandler(object arg0, string arg1)
        {
            PushState(new Quit(this, _sceneManagerWrapper));

            Application.Quit();
        }

        private void QuitCurrentGameEventHandler(object arg0, string arg1)
        {
            _stateStack.Clear();
            PushState(new Init(this, _sceneManagerWrapper));
        }

        private void ResumeGameEventHandler(object arg0, string arg1)
        {
            PopState();
        }

        private void StartGameEventHandler(object arg0, string arg1)
        {
            ReplaceState(new Play(this, _sceneManagerWrapper));
        }

        private void PauseGameEventHandler(object arg0, string arg1)
        {
            PushState(new Pause(this, _sceneManagerWrapper));
        }

        #endregion

        #region Handler methods for StateStack

        protected virtual void ReplaceState(State state)
        {
            PopState();

            PushState(state);
        }

        protected virtual void PushState(State state)
        {
            _stateStack.Push(state);
        }

        protected virtual void PopState()
        {
            var state = _stateStack.Pop();
        }

        #endregion

        //#region Event Handlers for State events

        ///// <summary>
        ///// Handle a request to pause the game
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //protected virtual void State_PauseGameEvent(object sender, EventArgs e)
        //{
        //    PushState(new Pause(this, _sceneManagerWrapper));
        //}

        ///// <summary>
        ///// Handle a request to resume a previously paused game
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //protected virtual void State_ResumeGameEvent(object sender, EventArgs e)
        //{
        //    PopState();
        //}

        ///// <summary>
        ///// Handle a request to open the Credits menu
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //private void State_CreditsEvent(object sender, EventArgs e)
        //{
        //    ReplaceState(new Credits(this, _sceneManagerWrapper));
        //}

        ///// <summary>
        ///// Handle a request to open the Help menu
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //private void State_HelpEvent(object sender, EventArgs e)
        //{
        //    ReplaceState(new Help(this, _sceneManagerWrapper));
        //}

        ///// <summary>
        ///// Handle a request to start a new game
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //protected virtual void State_PlayGameEvent(object sender, EventArgs e)
        //{
        //    ReplaceState(new Play(this, _sceneManagerWrapper));
        //}

        ///// <summary>
        ///// Handle a request to quit the current game
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //protected virtual void State_QuitCurrentGameEvent(object sender, EventArgs e)
        //{
        //    _stateStack.Clear();
        //    PushState(new Init(this, _sceneManagerWrapper));
        //}

        ///// <summary>
        ///// Handle a request to close the game and return to the OS
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //protected virtual void State_QuitGameEvent(object sender, EventArgs e)
        //{
        //    PushState(new Quit(this, _sceneManagerWrapper));

        //    Application.Quit();
        //}

        ///// <summary>
        ///// Handle a request to return to the Main menu
        ///// </summary>
        ///// <param name="sender">Event source</param>
        ///// <param name="e"></param>
        //protected virtual void State_BackToMainMenuEvent(object sender, EventArgs e)
        //{
        //    ReplaceState(new Init(this, _sceneManagerWrapper));
        //}

        //#endregion

        #region Input Event Handlers

        /// <summary>
        /// Event Handler for PauseResume actions
        /// Invokes PauseResume on the current state
        /// <remarks>This method is not testable, as the input parameter cannot be mocked</remarks>
        /// </summary>
        /// <param name="context"></param>
        public void OnPauseResumeGame(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    //Debug.Log($"{context.action} Performed");

                    _stateStack.Peek()?.PauseResumeGame();
                    break;
            }
        }

        #endregion
    }
}
