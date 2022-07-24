using InFlammis.Victoria.Assets.Scripts.MessageBroker;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Game/Static Objects Container", fileName = "StaticObjects")]
    public class StaticObjectsSO : ScriptableObject
    {
        #region Inspector
        [Header("References")]
        [SerializeField] private Messenger _Messenger;

        [Header("Global Settings")]
        [SerializeField] private bool skipIntro;
        #endregion

        #region Properties
        public IMessenger Messenger => _Messenger;
        public bool SkipIntro => skipIntro;
        #endregion
    }
}
