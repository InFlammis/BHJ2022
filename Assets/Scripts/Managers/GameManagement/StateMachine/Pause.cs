using InFlammis.Victoria.Assets.Scripts.Managers.Menus.Pause;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement.StateMachine
{
    public class Pause : State
    {
        public readonly string _sceneName = "PauseMenu";

        protected IPauseMenuManager _menuManager;

        public Pause(
            IGameManager gameManager,
            IUnitySceneManagerWrapper sceneManagerWrapper
        ) : base(gameManager, sceneManagerWrapper) { }

        private float _timeScale;

        public override void OnEnter()
        {
            base.OnEnter();

            _timeScale = Time.timeScale;
            SetTimeScale();

            SceneManagerWrapper.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override void OnExit()
        {
            base.OnExit();

            SceneManagerWrapper.UnloadSceneAsync(_sceneName);

            ResetTimeScale();
        }

        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _menuManager = GetMenuManagerFromScene(scene);

            if (_menuManager == null)
                return;

            base.SceneLoaded(scene, loadSceneMode);
        }

        protected virtual IPauseMenuManager GetMenuManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var menuManager = sceneManagerGo.GetComponent<PauseMenuManager>();
            return menuManager;
        }

        public override void PauseResumeGame()
        {
            base.PauseResumeGame();

            GameManager.StaticObjects.Messenger.PublishResumeGame(this, null);
        }

        private void SetTimeScale()
        {
            Time.timeScale = 1;
        }
        
        private void ResetTimeScale()
        {
            Time.timeScale = _timeScale;
        }
    }
}
