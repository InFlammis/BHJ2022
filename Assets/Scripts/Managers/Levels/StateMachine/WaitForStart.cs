using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /// <inheritdoc/>
        public override event EventHandler<State> ChangeStateRequestEvent;

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();
            var manager = Configuration.LevelManagerCore.LevelManager as MonoBehaviour;
            manager.StartCoroutine(CoChangeState(new Play(Configuration)));
            //Configuration.LevelManagerCore.LevelManager.StartCoroutine(CoChangeState(new Play(Configuration)));
        }

        /// <summary>
        /// Coroutine that manages the state change.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected IEnumerator CoChangeState(State state)
        {
            yield return new WaitForSeconds(ChangeStateDelay);
            ChangeStateRequestEvent?.Invoke(this, state);
        }
    }
}
