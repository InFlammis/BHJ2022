using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    public class MainMenuManager : MenuManager, IMainMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler StartGameEvent;

        /// <inheritdoc/>
        public event EventHandler QuitGameEvent;

        /// <inheritdoc/>
        public event EventHandler<Sound> PlaySoundEvent;

        /// <inheritdoc/>
        public event EventHandler CreditsEvent;

        /// <inheritdoc/>
        public event EventHandler HelpEvent;

        /// <inheritdoc/>
        public IMainMenuManager Core { get; protected set; }

        void Awake()
        {
            Core = new MainMenuManagerCore(this);

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

            Core.QuitGameEvent += (sender, args) => QuitGameEvent?.Invoke(sender, args);
            Core.StartGameEvent += (sender, args) => StartGameEvent?.Invoke(sender, args);
            Core.CreditsEvent += (sender, args) => CreditsEvent?.Invoke(sender, args);
            Core.HelpEvent += (sender, args) => HelpEvent?.Invoke(sender, args);
        }

        /// <inheritdoc/>
        public void StartGame()
        {
            Core.StartGame();
        }

        /// <inheritdoc/>
        public void QuitGame()
        { 
            Core.QuitGame();
        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            PlaySoundEvent?.Invoke(this, sound);
        }

        /// <inheritdoc/>
        public void ShowCredits()
        {
            Core.ShowCredits();
        }

        /// <inheritdoc/>
        public void ShowHelp()
        {
            Core.ShowHelp();
        }
    }
}
