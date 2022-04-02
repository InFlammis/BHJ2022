using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Pause
{
    public class PauseMenuManager : MenuManager, IPauseMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler ResumeGameEvent;

        /// <inheritdoc/>
        public event EventHandler QuitCurrentGameEvent;

        /// <inheritdoc/>
        public event EventHandler<Sound> PlaySoundEvent;

        public IPauseMenuManager Core { get; protected set; }

        void Awake()
        {
            Core = new PauseMenuManagerCore(this);

            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            Core.OnStart();
        }

        /// <inheritdoc/>
        public void OnAwake()
        {
            Core.OnAwake();

            Core.ResumeGameEvent += (sender, args) => ResumeGameEvent?.Invoke(sender, args);
            Core.QuitCurrentGameEvent += (sender, args) => QuitCurrentGameEvent?.Invoke(sender, args);
        }

        /// <inheritdoc/>
        public void ResumeGame()
        {
            Core.ResumeGame();
        }

        /// <inheritdoc/>
        public void QuitCurrentGame()
        {
            Core.QuitCurrentGame();
        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            PlaySoundEvent?.Invoke(this, sound);
        }

    }
}
