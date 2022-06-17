using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Credits
{
    /// <summary>
    /// Interface for the CreditsMenuManager
    /// </summary>
    public interface ICreditsMenuManager
    {
        public StaticObjectsSO StaticObjects { get; }

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
