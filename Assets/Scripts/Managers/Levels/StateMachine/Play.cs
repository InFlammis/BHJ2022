using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Levels.StateMachine
{
    public class Play : State
    {
        public Play(StateConfiguration configuration) : base(configuration)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Configuration.LevelManagerCore.EnablePlayerInput();

            if (Configuration.SpawnEnemiesEnabled)
            {
                Configuration.Messenger.PublishGameStarted(this, null);
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            Configuration.Messenger.PublishGameOver(this, null);
        }
    }
}
