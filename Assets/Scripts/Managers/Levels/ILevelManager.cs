using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    /// <summary>
    /// Interface of a generic LevelManager
    /// </summary>
    public interface ILevelManager : IMyMonoBehaviour
    {
        public StaticObjectsSO StaticObjects { get; }

        /// <summary>
        /// Reference to the PlayerControllerCore instance
        /// </summary>
        IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <summary>
        /// Invoked on start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on awake
        /// </summary>
        void OnAwake();

        /// <summary>
        /// Event Handler for a move action
        /// </summary>
        /// <param name="context"></param>
        void Move(InputAction.CallbackContext context);

        /// <summary>
        /// Disable the responsiveness to the Player input
        /// </summary>
        void DisablePlayerInput();

        /// <summary>
        /// Enable the responsiveness to the Player input
        /// </summary>
        void EnablePlayerInput();
    }
}