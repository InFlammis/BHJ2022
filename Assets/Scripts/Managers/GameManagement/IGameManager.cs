using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement
{
    /// <summary>
    /// Interface describing a GameManager
    /// </summary>
    public interface IGameManager
    {
        public StaticObjectsSO StaticObjects { get; }

        #region Unity Methods

        void OnAwake();

        void OnStart();

        #endregion

        /// <summary>
        /// EventHandler for a request to Pause/Resume a game
        /// </summary>
        /// <param name="context"></param>
        void OnPauseResumeGame(InputAction.CallbackContext context);
    }
}