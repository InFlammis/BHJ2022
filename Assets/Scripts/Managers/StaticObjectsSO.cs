using BulletHellJam2022.Assets.Scripts.MessageBroker;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Game/Static Objects Container", fileName = "StaticObjects")]
    public class StaticObjectsSO : ScriptableObject
    {
        [SerializeField] private Messenger _Messenger;

        public IMessenger Messenger => _Messenger;
    }
}
