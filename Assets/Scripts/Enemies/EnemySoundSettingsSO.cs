using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    [CreateAssetMenu(menuName = "Game/Settings/Sound Settings", fileName = "Sound Settings")]
    public class EnemySoundSettingsSO : ScriptableObject
    {
        [SerializeField]
        private Sound _moveSound;

        [SerializeField]
        private Sound _explodeSound;

        [SerializeField]
        private Sound _hitSound;

        public Sound MoveSound => _moveSound;
        public Sound ExplodeSound => _explodeSound;
        public Sound HitSound => _hitSound;
    }
}
