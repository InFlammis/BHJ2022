using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies
{
    [CreateAssetMenu(menuName = "Game/Init Settings/Enemy Sound Settings", fileName = "EnemySoundSettings")]
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
