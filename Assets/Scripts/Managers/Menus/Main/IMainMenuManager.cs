using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Main
{
    public interface IMainMenuManager
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