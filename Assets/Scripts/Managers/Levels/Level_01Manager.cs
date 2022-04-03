﻿using System;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    public class Level_01Manager : LevelManager
    {
        /// <inheritdoc/>
        public ILevelManagerCore Core { get; protected set; }
        private PlayerInput _playerInput;

        ///// <inheritdoc/>
        //public override event EventHandler<Sound> PlaySoundEvent;

        ///// <inheritdoc/>
        //public override event Action ReturnToMainEvent;

        void Awake()
        {
            Core = new Level_01ManagerCore(this);

            OnAwake();

            (_staticObjects.Messenger as IPlayerEventsMessenger).HasDied.AddListener(this.PlayerHasDied);
            _staticObjects.Messenger.OrchestrationComplete.AddListener(this.OrchestrationManagerOrchestrationComplete);
            _staticObjects.Messenger.ResumeGame.AddListener(this.ResumeGameEventHandler);
        }

        void Start()
        {
            OnStart();
        }

        private void ResumeGameEventHandler(object arg0, string arg1)
        {
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            base.OnStart();
            Core.OnStart();
        }

        /// <inheritdoc/>
        public void OnAwake()
        {
            Core.OnAwake();
        }

        /// <inheritdoc/>
        public override void Move(InputAction.CallbackContext context)
        {
            Core.Move(context);
        }

        /// <inheritdoc/>
        public override void DisablePlayerInput()
        {
            Core.DisablePlayerInput();
        }

        /// <inheritdoc/>
        public override void EnablePlayerInput()
        {
            Core.EnablePlayerInput();
        }

        ///// <inheritdoc/>
        //public void ReturnToMain()
        //{
        //    StaticObjects.Messenger.PublishQuitCurrentGame(this, null);
        //}

        public void PlayerHasDied(object publisher, string target)
        {
            Core.PlayerHasDied(publisher, target);
        }

        public void OrchestrationManagerOrchestrationComplete(object publisher, string target)
        {
            Core.OrchestrationManagerOrchestrationComplete(publisher, target);
        }

    }
}
