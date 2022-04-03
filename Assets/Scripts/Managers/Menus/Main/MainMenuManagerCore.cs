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
        public event EventHandler CreditsEvent;

        /// <inheritdoc/>
        public event EventHandler HelpEvent;

        /// <inheritdoc/>
        public readonly IMainMenuManager Parent;

        public StaticObjectsSO StaticObjects => Parent.StaticObjects;

        public MainMenuManagerCore(IMainMenuManager parent)
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
            //StaticObjects.Messenger.PublishStartGame(this, null);
        }

        /// <inheritdoc/>
        public void QuitGame()
        {
            //StaticObjects.Messenger.PublishQuitGame(this, null);
        }

        /// <inheritdoc/>
        public void ShowCredits()
        {
            //StaticObjects.Messenger.PublishOpenCreditsMenu(this, null);
        }

        /// <inheritdoc/>
        public void ShowHelp()
        {
            //StaticObjects.Messenger.PublishOpenHelpMenu(this, null);
        }
    }
}
