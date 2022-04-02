using System;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    public class Level_01Manager : LevelManager
    {
        /// <inheritdoc/>
        public ILevelManagerCore Core { get; protected set; }
        private PlayerInput _playerInput;

        /// <inheritdoc/>
        public override event EventHandler<Sound> PlaySoundEvent;

        /// <inheritdoc/>
        public override event Action ReturnToMainEvent;

        void Awake()
        {
            Core = new Level_01ManagerCore(this);

            OnAwake();
        }

        void Start()
        {
            OnStart();
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

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            PlaySoundEvent?.Invoke(this, sound);
        }

        /// <inheritdoc/>
        public override void ReturnToMain()
        {
            ReturnToMainEvent?.Invoke();
        }

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
