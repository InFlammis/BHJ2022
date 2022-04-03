using BulletHellJam2022.Assets.Scripts.Managers.Menus.Main;
using System.Linq;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    /// <summary>
    /// Manage the initial MainMenu
    /// </summary>
    public class Init : State
    {
        ///// <inheritdoc/>
        //public override event EventHandler PauseGameEvent;

        ///// <inheritdoc/>
        //public override event EventHandler ResumeGameEvent;

        ///// <inheritdoc/>
        //public override event EventHandler PlayGameEvent;

        ///// <inheritdoc/>
        //public override event EventHandler QuitCurrentGameEvent;

        ///// <inheritdoc/>
        //public override event EventHandler QuitGameEvent;

        ///// <inheritdoc/>
        //public override event EventHandler CreditsEvent;

        ///// <inheritdoc/>
        //public override event EventHandler BackToMainMenuEvent;

        ///// <inheritdoc/>
        //public override event EventHandler HelpEvent;

        /// <summary>
        /// Name of the scene to open
        /// </summary>
        public readonly string _sceneName = "MainMenu";

        ///// <summary>
        ///// Reference to the IMainMenuManager instance
        ///// </summary>
        //protected IMainMenuManager _menuManager;

        /// <summary>
        /// Create a new instance of the class
        /// </summary>
        /// <param name="gameManager">Reference to the GameManager <see cref="IGameManager"/></param>
        /// <param name="sceneManagerWrapper">Reference to the SceneManagerWrapper <see cref="IUnitySceneManagerWrapper"/></param>
        public Init(
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="loadSceneMode"></param>
        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            //_menuManager = GetMenuManagerFromScene(scene);
            //if (_menuManager == null)
            //    return;

            base.SceneLoaded(scene, loadSceneMode);

            //_menuManager.StartGameEvent += StartGameEventHandler;
            //_menuManager.QuitGameEvent += QuitGameEventHandler;
            //_menuManager.CreditsEvent += CreditsEventHandler;
            //_menuManager.HelpEvent += HelpEventHandler;

            
        }

        //This method is non-testable because it accesses Scene's methods and GameObject's methods, which are not mockable.
        protected virtual IMainMenuManager GetMenuManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var menuManager = sceneManagerGo.GetComponent<MainMenuManager>();
            return menuManager;
        }

        ///// <inheritdoc/>
        //protected virtual void StartGameEventHandler(object sender, EventArgs state)
        //{
        //    PlayGameEvent?.Invoke(this, state);
        //}

        ///// <inheritdoc/>
        //protected virtual void QuitGameEventHandler(object sender, EventArgs state)
        //{
        //    QuitGameEvent?.Invoke(this, state);
        //}

        ///// <inheritdoc/>
        //protected virtual void CreditsEventHandler(object sender, EventArgs state)
        //{
        //    CreditsEvent?.Invoke(this, state);
        //}

        ///// <inheritdoc/>
        //protected virtual void HelpEventHandler(object sender, EventArgs state)
        //{
        //    HelpEvent?.Invoke(this, state);
        //}
    }
}
