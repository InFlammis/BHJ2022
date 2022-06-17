using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine.InputSystem;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement
{
    public interface IGameManager
    {
        public StaticObjectsSO StaticObjects { get; }

        #region Unity Methods

        void OnAwake();

        void OnStart();

        #endregion

        void OnPauseResumeGame(InputAction.CallbackContext context);
    }
}