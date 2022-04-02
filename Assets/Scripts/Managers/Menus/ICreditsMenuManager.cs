using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    /// <summary>
    /// Interface for the CreditsMenuManager
    /// </summary>
    public interface ICreditsMenuManager
    {
        /// <summary>
        /// Event raised by the Back button
        /// </summary>
        event EventHandler BackEvent;

        /// <summary>
        /// Event raised to play a sound
        /// </summary>
        event EventHandler<Sound> PlaySoundEvent;

        /// <summary>
        /// Invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on Awake
        /// </summary>
        void OnAwake();

        /// <summary>
        /// Invoked to return to the Main Menu
        /// </summary>
        void BackToMainMenu();
    }
}
