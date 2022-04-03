using BulletHellJam2022.Assets.Scripts.Managers.Menus.Credits;
using System.Linq;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    public class Credits : State
    {
        public readonly string _sceneName = "CreditsMenu";

        protected ICreditsMenuManager _menuManager;

        public Credits(
            IGameManager gameManager, 
            IUnitySceneManagerWrapper sceneManagerWrapper
            ) : base(gameManager, sceneManagerWrapper)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            SceneManagerWrapper.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override void OnExit()
        {
            base.OnExit();

            SceneManagerWrapper.UnloadSceneAsync(_sceneName);
        }

        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _menuManager = GetMenuManagerFromScene(scene);
            if (_menuManager == null)
                return;

            base.SceneLoaded(scene, loadSceneMode);
        }

        protected virtual ICreditsMenuManager GetMenuManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var menuManager = sceneManagerGo.GetComponent<CreditsMenuManager>();
            return menuManager;
        }
    }
}
