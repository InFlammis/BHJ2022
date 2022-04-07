using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine
{
    public class GameOver : State
    {
        private float _returnToMainDelay = 8;

        public GameOver(StateConfiguration configuration) : base(configuration)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Configuration.Messenger.PublishSetCentralMessage(this, null, "Game Over");

            Configuration.LevelManagerCore.LevelManager.StartCoroutine(CoReturnToMain());
        }

        public IEnumerator CoReturnToMain()
        {
            yield return new WaitForSeconds(_returnToMainDelay);
            Configuration.Messenger.PublishQuitCurrentGame(this, null);
        }


        public override void OnExit()
        {
            base.OnExit();
            Configuration.Messenger.PublishSetCentralMessage(this, null, String.Empty);
        }
    }
}
