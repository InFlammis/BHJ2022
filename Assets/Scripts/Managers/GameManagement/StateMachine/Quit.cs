namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
{
    public class Quit : State
    {
        public Quit(
            IGameManager gameManager,
            IUnitySceneManagerWrapper sceneManagerWrapper
        ) : base(gameManager, sceneManagerWrapper) { }

        public override void OnActivate()
        {
            base.OnActivate();
        }
    }
}
