using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement
{
    public class GameManager : SceneManager, IGameManager
    {
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

        public void PauseResumeGame(InputAction.CallbackContext context)
        {
            OnPauseResumeGame(context);
        }

        #endregion

        public void OnAwake()
        {
            Core = new GameManagerCore(this);
            Core.OnAwake();
        }

        public void OnStart()
        {
            Core.OnStart();
        }

        public void OnPauseResumeGame(InputAction.CallbackContext context)
        {
            Core.OnPauseResumeGame(context);
        }
    }
}
