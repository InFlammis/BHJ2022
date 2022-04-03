using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    [CreateAssetMenu(menuName = "Game/Init Settings/Level Sound Settings", fileName = "LevelSoundSettings")]
    public class LevelSceneSoundSettingsSO : ScriptableObject
    {
        [SerializeField]
        private Sound _backgroundMusic;

        public Sound BackgroundMusic => _backgroundMusic;
    }
}
