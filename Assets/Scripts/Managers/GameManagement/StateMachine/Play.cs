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

            GameManager.StaticObjects.Messenger.PublishPauseGame(this, null);
        }
    }
}
