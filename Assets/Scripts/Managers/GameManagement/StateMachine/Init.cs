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
        /// <summary>
        /// Name of the scene to open
        /// </summary>
        public readonly string _sceneName = "MainMenu";

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
            base.SceneLoaded(scene, loadSceneMode);
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
    }
}
