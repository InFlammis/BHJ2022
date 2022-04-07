﻿using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine
{
    public class Win : State
    {
        private float _returnToMainDelay = 8;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="configuration">Initial state configuration</param>
        public Win(StateConfiguration configuration) : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();
            (Configuration.Messenger as IHudEventsPublisher).PublishSetCentralMessage(this, null, "You Won!");
            Configuration.LevelManagerCore.DisablePlayerInput();
            Configuration.LevelManagerCore.LevelManager.StartCoroutine(CoReturnToMain());
        }

        /// <summary>
        /// CoRoutine that manages the return to main menu
        /// </summary>
        /// <returns></returns>
        public IEnumerator CoReturnToMain()
        {
            yield return new WaitForSeconds(_returnToMainDelay);
            Configuration.Messenger.PublishBackToMain(this, null);
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();
            (Configuration.Messenger as IHudEventsPublisher).PublishSetCentralMessage(this, null, String.Empty);
        }
    }
}
