using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Main
{
    public class MainMenuManagerCore : IMainMenuManager
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
        public readonly IMyMonoBehaviour Parent;

        public MainMenuManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            //Debug.Log($"Main menu opened");
        }

        /// <inheritdoc/>
        public void OnAwake() { }

        /// <inheritdoc/>
        public void StartGame()
        {
            StartGameEvent?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public void QuitGame()
        {
            QuitGameEvent?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public void ShowCredits()
        {
            CreditsEvent?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public void ShowHelp()
        {
            HelpEvent?.Invoke(this, new EventArgs());
        }
    }
}
