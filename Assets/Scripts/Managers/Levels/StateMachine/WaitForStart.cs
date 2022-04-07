using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine
{
    /// <summary>
    /// State that manages the phase before the games start
    /// </summary>
    public class WaitForStart : State
    {
        public WaitForStart(StateConfiguration configuration) : base(configuration)
        {
            ChangeStateDelay = 2;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            var manager = Configuration.LevelManagerCore.LevelManager as MonoBehaviour;
            manager.StartCoroutine(CoChangeState(new Play(Configuration)));
        }

        /// <summary>
        /// Coroutine that manages the state change.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected IEnumerator CoChangeState(State state)
        {
            yield return new WaitForSeconds(ChangeStateDelay);
            Configuration.Messenger.PublishLevelChangeStateRequest(this, null, state);
        }
    }
}
