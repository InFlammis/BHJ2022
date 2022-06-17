using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Pause
{
    /// <summary>
    /// Interface for the PauseMenuManager
    /// </summary>
    public interface IPauseMenuManager
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
        /// Invoked on Resume Game button hit
        /// </summary>
        void ResumeGame();

        /// <summary>
        /// Invoked on Quit Current Game button hit
        /// </summary>
        void QuitCurrentGame();
    }
}