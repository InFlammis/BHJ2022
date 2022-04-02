using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    public interface IMainMenuManager
    {
        /// <summary>
        /// Event raised on Start Game button click
        /// </summary>
        event EventHandler StartGameEvent;

        /// <summary>
        /// Event raised on Credits button click
        /// </summary>
        event EventHandler CreditsEvent;
        
        /// <summary>
        /// Event raised on Quit Game button click
        /// </summary>
        event EventHandler QuitGameEvent;

        /// <summary>
        /// Event raised to play a sound
        /// </summary>
        event EventHandler<Sound> PlaySoundEvent;

        /// <summary>
        /// Event raised on Help button click
        /// </summary>
        event EventHandler HelpEvent;

        /// <summary>
        /// Invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on Awake
        /// </summary>
        void OnAwake();

        /// <summary>
        /// Invoked on Start Game button click
        /// </summary>
        void StartGame();

        /// <summary>
        /// Invoked on Credits button click
        /// </summary>
        void ShowCredits();

        /// <summary>
        /// Invoked on Quit button click
        /// </summary>
        void QuitGame();

        /// <summary>
        /// Invoked on Help button click
        /// </summary>
        void ShowHelp();
    }
}