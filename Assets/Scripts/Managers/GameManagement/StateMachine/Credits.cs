using BulletHellJam2022.Assets.Scripts.Managers.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    /// <summary>
    /// Manage the Credits menu
    /// </summary>
    public class Credits : State
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
        public readonly string _sceneName = "CreditsMenu";

        /// <summary>
        /// Reference to the ICreditMenuManager instance
        /// </summary>
        protected ICreditsMenuManager _menuManager;

        /// <summary>
        /// Create a new instance of the class
        /// </summary>
        /// <param name="gameManager">Reference to the GameManager <see cref="IGameManager"/></param>
        /// <param name="sceneManagerWrapper">Reference to the SceneManagerWrapper <see cref="IUnitySceneManagerWrapper"/></param>
        public Credits(
            IGameManager gameManager, 
            IUnitySceneManagerWrapper sceneManagerWrapper
            ) : base(gameManager, sceneManagerWrapper)
        {
        }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();

            SceneManagerWrapper.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            GameManager.SoundManager.PlayMusic(GameManager.SoundManager.MenuMusic);
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();

            SceneManagerWrapper.UnloadSceneAsync(_sceneName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="loadSceneMode"></param>
        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _menuManager = GetMenuManagerFromScene(scene);
            if (_menuManager == null)
                return;

            base.SceneLoaded(scene, loadSceneMode);

            _menuManager.PlaySoundEvent += MenuManager_PlaySoundEvent;
            _menuManager.BackEvent += BackEventHandler;
        }

        /// <summary>
        /// EventHandler that manages a request to Play a sound
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Sound to play</param>
        private void MenuManager_PlaySoundEvent(object sender, SoundManagement.Sound e)
        {
            GameManager.SoundManager.PlaySound(e);
        }

        /// <inheritdoc/>
        protected virtual ICreditsMenuManager GetMenuManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var menuManager = sceneManagerGo.GetComponent<CreditsMenuManager>();
            return menuManager;
        }

        /// <inheritdoc/>
        protected virtual void BackEventHandler(object sender, EventArgs state)
        {
            BackToMainMenuEvent?.Invoke(this, state);
        }

    }
}
