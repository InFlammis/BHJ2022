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
    /// State that manages the Play phase
    /// </summary>
    public class Play : State
    {
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="configuration">State configuration</param>
        public Play(StateConfiguration configuration) : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();

            Configuration.LevelManagerCore.EnablePlayerInput();

            if (Configuration.SpawnEnemiesEnabled)
            {
                Configuration.Messenger.PublishGameStarted(this, null);
            }

        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();

            Configuration.Messenger.PublishGameOver(this, null);
        }

        /// <inheritdoc/>
        public override event EventHandler<State> ChangeStateRequestEvent;
    }
}
