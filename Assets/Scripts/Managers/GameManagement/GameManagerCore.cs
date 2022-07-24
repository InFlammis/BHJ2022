using System;
using InFlammis.Victoria.Assets.Scripts.Managers.GameManagement.StateMachine;
using InFlammis.Victoria.Assets.Scripts.Managers.SceneManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement
{
    public class GameManagerCore : IGameManager
    {
        public readonly IMyMonoBehaviour Parent;

        private IUnitySceneManagerWrapper _sceneManagerWrapper;

        protected StateStack _stateStack = new StateStack();

        public ISoundManager SoundManager { get; protected set; }

        public StaticObjectsSO StaticObjects => (Parent as IGameManager).StaticObjects;

        public GameManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
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
            StaticObjects.Messenger.PreRollFinished.AddListener(PreRollFinishedEventHandler);

        }
        public void OnStart()
        {
            //PushState(new Init(this, _sceneManagerWrapper));
            //PushState(new Pause(this, _sceneManagerWrapper));
            PushState(new Init(this, _sceneManagerWrapper));

        }

        #endregion

        #region Event Handlers for StateStack events

        protected virtual void StateStack_PushingStateEvent(object sender, State state)
        {
        }

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

        private void PreRollFinishedEventHandler(object arg0, string arg1)
        {
            ReplaceState(new Init(this, _sceneManagerWrapper));
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

        #region Pause

        public void OnPauseResumeGame()
        {
            _stateStack.Peek()?.PauseResumeGame();
        }
        #endregion
    }
}
