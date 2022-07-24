using InFlammis.Victoria.Assets.Scripts.Managers.SceneManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.GameManagement.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement
{
    public class GameManager : SceneManager, IGameManager
    {
        #region Inspector
        #endregion

        #region Interfaces

        public IGameManager Core { get; protected set; }
        
        #endregion

        #region Unity methods
        
        void Awake()
        {
            OnAwake();
        }

        void Start()
        {
            OnStart();
        }
        
        #endregion

        #region Internal methods
        
        public void OnAwake()
        {
            Core = new GameManagerCore(this);
            Core.OnAwake();
        }

        public void OnStart()
        {
            Core.OnStart();
        }

        public void OnPauseResumeGame()
        {
            Core.OnPauseResumeGame();
        }
        
        #endregion
    }
}
