using InFlammis.Victoria.Assets.Scripts.MessageBroker;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Game/Static Objects Container", fileName = "StaticObjects")]
    public class StaticObjectsSO : ScriptableObject
    {
        [SerializeField] private Messenger _Messenger;

        public IMessenger Messenger => _Messenger;
    }
}
