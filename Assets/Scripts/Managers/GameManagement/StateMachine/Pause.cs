using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Managers.Menus;
using BulletHellJam2022.Assets.Scripts.Managers.Menus.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    /// <summary>
    /// Manage the Pause/Resume of a game
    /// </summary>
    public class Pause : State
    {
        /// <inheritdoc/>
        public override event EventHandler PauseGameEvent;

        /// <inheritdoc/>
        public override event EventHandler ResumeGameEvent;

        /// <inheritdoc/>
        public override event EventHandler PlayGameEvent;

        /// <inheritdoc/>
        public override event EventHandler QuitCurrentGameEvent;

        /// <inheritdoc/>
        public override event EventHandler QuitGameEvent;

        /// <inheritdoc/>
        public override event EventHandler CreditsEvent;

        /// <inheritdoc/>
        public override event EventHandler BackToMainMenuEvent;

        /// <inheritdoc/>
        public override event EventHandler HelpEvent;


        /// <summary>
        /// Name of the scene to open
        /// </summary>
        public readonly string _sceneName = "PauseMenu";

        /// <summary>
        /// Reference to the IPauseMenuManager instance
        /// </summary>
        protected IPauseMenuManager _menuManager;

        /// <summary>
        /// Create a new instance of the class
        /// </summary>
        /// <param name="gameManager">Reference to the GameManager <see cref="IGameManager"/></param>
        /// <param name="sceneManagerWrapper">Reference to the SceneManagerWrapper <see cref="IUnitySceneManagerWrapper"/></param>
        public Pause(
            IGameManager gameManager,
            IUnitySceneManagerWrapper sceneManagerWrapper
        ) : base(gameManager, sceneManagerWrapper) { }

        /// <summary>
        /// The initial time scale. Used to freeze the game and resume
        /// </summary>
        private float _timeScale;

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();

            _timeScale = Time.timeScale;
            SetTimeScale();

            SceneManagerWrapper.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();

            SceneManagerWrapper.UnloadSceneAsync(_sceneName);

            ResetTimeScale();
        }

        /// <inheritdoc/>
        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _menuManager = GetMenuManagerFromScene(scene);

            if (_menuManager == null)
                return;

            base.SceneLoaded(scene, loadSceneMode);

            //_menuManager.PlaySoundEvent += MenuManager_PlaySoundEvent;
            //_menuManager.ResumeGameEvent += ResumeGameEventHandler;
            //_menuManager.QuitCurrentGameEvent += QuitCurrentGameEventHandler;
        }

        ///// <summary>
        ///// EventHandler that manages a request to Play a sound
        ///// </summary>
        ///// <param name="sender">Event sender</param>
        ///// <param name="e">Sound to play</param>
        //private void MenuManager_PlaySoundEvent(object sender, SoundManagement.Sound e)
        //{
        //    GameManager.StaticObjects.Messenger.PublishPlaySound(this, null, e);
        //}

        //This method is non-testable because it accesses Scene's methods and GameObject's methods, which are not mockable.
        protected virtual IPauseMenuManager GetMenuManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var menuManager = sceneManagerGo.GetComponent<PauseMenuManager>();
            return menuManager;
        }

        ///// <summary>
        ///// Manages a request to resume the game after the pause
        ///// </summary>
        ///// <param name="sender">Event sender</param>
        ///// <param name="state"></param>
        //protected void ResumeGameEventHandler(object sender, EventArgs state)
        //{
        //    ResumeGameEvent?.Invoke(this, new EventArgs());
        //}

        ///// <summary>
        ///// Manages a request to quit the current game after the pause
        ///// </summary>
        ///// <param name="sender">Event sender</param>
        ///// <param name="state"></param>
        //protected void QuitCurrentGameEventHandler(object sender, EventArgs state)
        //{
        //    QuitCurrentGameEvent?.Invoke(this, new EventArgs());
        //}

        /// <inheritdoc/>
        public override void PauseResumeGame()
        {
            base.PauseResumeGame();

            //ResumeGameEvent?.Invoke(this, new EventArgs());
            GameManager.StaticObjects.Messenger.PublishResumeGame(this, null);
        }

        /// <summary>
        /// Slows down the time speed to zero, to freeze the game
        /// </summary>
        private void SetTimeScale()
        {
            Time.timeScale = 0;
        }
        
        /// <summary>
        /// Resets the time speed to the original value, to resume the game
        /// </summary>
        private void ResetTimeScale()
        {
            Time.timeScale = _timeScale;
        }
    }
}
