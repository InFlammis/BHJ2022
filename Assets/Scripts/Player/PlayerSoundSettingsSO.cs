using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Player
{
    [CreateAssetMenu(menuName = "Game/Init Settings/Player Sound Settings", fileName = "PlayerSoundSettings")]

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
