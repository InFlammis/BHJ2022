using BulletHellJam2022.Assets.Scripts.Managers.Levels;
using System.Linq;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    public class Play : State
    {
        public readonly string _sceneName = "Level_01";

        protected ILevelManager _levelManager;
        public Play(
            IGameManager gameManager,
            IUnitySceneManagerWrapper sceneManagerWrapper
        ) : base(gameManager, sceneManagerWrapper) { }

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

        public override void OnActivate()
        {
            base.OnActivate();

            _levelManager?.EnablePlayerInput();

        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();

            _levelManager?.DisablePlayerInput();
        }

        public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _levelManager = GetSceneManagerFromScene(scene);

            if (_levelManager == null)
                return;

            base.SceneLoaded(scene, loadSceneMode);
        }

        protected virtual ILevelManager GetSceneManagerFromScene(Scene scene)
        {
            if (scene.name != _sceneName)
                return null;

            var rootGameObjects = scene.GetRootGameObjects();
            var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            var levelManager = sceneManagerGo.GetComponent<Level_01Manager>();

            return levelManager;
        }

        public override void PauseResumeGame()
        {
            base.PauseResumeGame();

            GameManager.StaticObjects.Messenger.PublishPauseGame(this, null);
        }
    }
}
