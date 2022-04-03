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
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
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
            //Core.ResumeGame();
            StaticObjects.Messenger.PublishResumeGame(this, null);

        }

        /// <inheritdoc/>
        public void QuitCurrentGame()
        {
            //Core.QuitCurrentGame();
            StaticObjects.Messenger.PublishQuitCurrentGame(this, null);

        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }

    }
}
