using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement
{
    /// <summary>
    /// Game manager. Coordinates the game and its components.
    /// </summary>
    public class GameManager : SceneManager, IGameManager
    {
        /// <summary>
        /// Reference to the GameManager core instance
        /// </summary>
        public IGameManager Core { get; protected set; }

        #region MonoBehaviour methods

        void Awake()
        {
            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        #endregion

        #region Input Event Handlers

        /// <summary>
        /// Event Handler for PauseResume actions
        /// Invokes PauseResume on the current state
        /// </summary>
        /// <param name="context"></param>
        public void PauseResumeGame(InputAction.CallbackContext context)
        {
            OnPauseResumeGame(context);
        }

        #endregion

        /// <summary>
        /// Invoked on Awake
        /// </summary>
        public void OnAwake()
        {
            Core = new GameManagerCore(this);
            Core.OnAwake();
        }

        /// <summary>
        /// Invoked on Start
        /// </summary>
        public void OnStart()
        {
            Core.OnStart();
        }

        /// <inheritdoc/>
        public void OnPauseResumeGame(InputAction.CallbackContext context)
        {
            Core.OnPauseResumeGame(context);
        }
    }
}
