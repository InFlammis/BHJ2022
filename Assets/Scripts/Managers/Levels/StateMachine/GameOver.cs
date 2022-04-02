using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
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
    /// State that manages the Game Over phase
    /// </summary>
    public class GameOver : State
    {
        /// <inheritdoc/>
        public override event EventHandler<State> ChangeStateRequestEvent;
        private float _returnToMainDelay = 8;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="configuration">Initial state configuration</param>
        public GameOver(StateConfiguration configuration) : base(configuration)
        {
        }


        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();
            Configuration.Messenger.PublishSetCentralMessage(this, null, "Game Over");

            Configuration.LevelManagerCore.LevelManager.StartCoroutine(CoReturnToMain());
        }

        /// <summary>
        /// CoRoutine that manages the return to main menu
        /// </summary>
        /// <returns></returns>
        public IEnumerator CoReturnToMain()
        {
            yield return new WaitForSeconds(_returnToMainDelay);
            Configuration.LevelManagerCore.LevelManager.ReturnToMain();
        }


        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();
            Configuration.Messenger.PublishSetCentralMessage(this, null, String.Empty);
        }
    }
}
