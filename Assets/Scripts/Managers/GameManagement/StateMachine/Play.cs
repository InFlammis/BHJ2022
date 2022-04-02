using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Managers.Levels;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    /// <summary>
    /// Manages the actions during the game play
    /// </summary>
    public class Play : State
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
        public readonly string _sceneName = "Level_01";

        /// <summary>
        /// Reference to the ILevelManager instance
        /// </summary>
        protected ILevelManager _levelManager;

        /// <summary>
        /// Create a new instance of the class
        /// </summary>
        /// <param name="gameManager">Reference to the GameManager <see cref="IGameManager"/></param>
        /// <param name="sceneManagerWrapper">Reference to the SceneManagerWrapper <see cref="IUnitySceneManagerWrapper"/></param>
        public Play(
            IGameManager gameManager,
            IUnitySceneManagerWrapper sceneManagerWrapper
        ) : base(gameManager, sceneManagerWrapper) { }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();

            SceneManagerWrapper.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            GameManager.SoundManager.PlayMusic(GameManager.SoundManager.GameMusic);
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();

            SceneManagerWrapper.UnloadSceneAsync(_sceneName);
        }

        /// <inheritdoc/>
        public override void OnActivate()
        {
            base.OnActivate();

            GameManager.SoundManager.PlayMusic(GameManager.SoundManager.GameMusic);

            _levelManager?.EnablePlayerInput();
        }

        /// <inheritdoc/>
        public override void OnDeactivate()
        {
            base.OnDeactivate();

            _levelManager?.DisablePlayerInput();
        }

        /// <inheritdoc/>
        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _levelManager = GetSceneManagerFromScene(scene);

            if (_levelManager == null)
                return;

            base.SceneLoaded(scene, loadSceneMode);

            //Bind event handlers here
            _levelManager.PlaySoundEvent += LevelManager_PlaySoundEvent;
            _levelManager.ReturnToMainEvent += LevelManager_ReturnToMain;
        }

        /// <summary>
        /// EventHandler that manages a request to Return to the main menu, quitting the current game
        /// </summary>
        private void LevelManager_ReturnToMain()
        {
            QuitCurrentGameEvent?.Invoke(this, null);
        }

        /// <summary>
        /// EventHandler that manages a request to Play a sound
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Sound to play</param>
        private void LevelManager_PlaySoundEvent(object sender, Sound e)
        {
            GameManager.SoundManager.PlaySound(e);
        }

        //This method is non-testable because it accesses Scene's methods and GameObject's methods, which are not mockable.
        /// <inheritdoc/>
        protected virtual ILevelManager GetSceneManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var levelManager = sceneManagerGo.GetComponent<Level_01Manager>();

            return levelManager;
        }

        /// <inheritdoc/>
        public override void PauseResumeGame()
        {
            base.PauseResumeGame();

            PauseGameEvent?.Invoke(this, new EventArgs());
        }
    }
}
