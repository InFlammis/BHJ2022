using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Main
{
    public class MainMenuManager : MenuManager, IMainMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler StartGameEvent;

        /// <inheritdoc/>
        public event EventHandler QuitGameEvent;

        /// <inheritdoc/>
        //public event EventHandler<Sound> PlaySoundEvent;

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
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
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
            StaticObjects.Messenger.PublishStartGame(this, null);

        }

        /// <inheritdoc/>
        public void QuitGame()
        { 
            Core.QuitGame();
            StaticObjects.Messenger.PublishQuitGame(this, null);

        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }

        /// <inheritdoc/>
        public void ShowCredits()
        {
            Core.ShowCredits();
            StaticObjects.Messenger.PublishOpenCreditsMenu(this, null);

        }

        /// <inheritdoc/>
        public void ShowHelp()
        {
            Core.ShowHelp();
            StaticObjects.Messenger.PublishOpenHelpMenu(this, null);
        }
    }
}
