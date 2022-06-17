﻿using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Help
{
    /// <summary>
    /// Interface for the HelpMenuManager
    /// </summary>
    public interface IHelpMenuManager
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
