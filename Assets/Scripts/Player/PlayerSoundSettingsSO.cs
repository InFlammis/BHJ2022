using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Player
{
    [CreateAssetMenu(menuName = "Game/Settings/Player Sound Settings", fileName = "Player Sound Settings")]

    public class PlayerSoundSettingsSO : ScriptableObject
    {
        [SerializeField]
        private Sound _moveSound;

        [SerializeField]
        private Sound _explodeSound;

        [SerializeField]
        private Sound _hitSound;

        [SerializeField]
        private Sound _powerUpSound;

        public Sound MoveSound => _moveSound;
        public Sound ExplodeSound => _explodeSound;
        public Sound HitSound => _hitSound;
        public Sound PowerUpSound => _powerUpSound;
    }
}
