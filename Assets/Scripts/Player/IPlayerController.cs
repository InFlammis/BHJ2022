using System.Collections.Generic;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Player
{
    /// <summary>
    /// Interface for a PlayerController class.
    /// </summary>
    public interface IPlayerController
    {
        IMessenger Messenger { get; }
        /// <summary>
        /// The parent GameObject
        /// </summary>
        GameObject GameObject { get; }

        /// <summary>
        /// Reference to the inner Controller Core
        /// </summary>
        IPlayerControllerCore Core { get; set; }

        /// <summary>
        /// Reference to the HealthManager instance
        /// </summary>
        IHealthManager HealthManager { get; }

        /// <summary>
        /// Collection of weapons available for the player
        /// </summary>
        WeaponBase[] Weapons { get; }

        /// <summary>
        /// Initial settings
        /// </summary>
        PlayerSettings InitSettings { get; }

        /// <summary>
        /// Invoked by the Input manager to start a move action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnMove(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start a fire action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnFire(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start an Alternate fire action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnFireAlt(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start a Open selection menu action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnOpenSelectionMenu(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start a turn left action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnTurnLeft(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start a turn right action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnTurnRight(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start a turn up action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnTurnUp(InputAction.CallbackContext context);

        /// <summary>
        /// Invoked by the Input manager to start a turn down action
        /// </summary>
        /// <param name="context">The inputAction context</param>
        void OnTurnDown(InputAction.CallbackContext context);
    }
}