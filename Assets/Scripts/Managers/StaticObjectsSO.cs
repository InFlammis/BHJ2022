using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Game/Static Objects Container", fileName = "StaticObjects")]
    public class StaticObjectsSO : ScriptableObject
    {
        [SerializeField] private SoundManager _SoundManager;
        [SerializeField] private Messenger _Messenger;

        public ISoundManager SoundManager => _SoundManager;
        public IMessenger Messenger => _Messenger;

        private void Awake()
        {
            _SoundManager.OnAwake();
        }

        private void OnEnable()
        {
            _SoundManager.OnEnable2();
        }
    }
}
