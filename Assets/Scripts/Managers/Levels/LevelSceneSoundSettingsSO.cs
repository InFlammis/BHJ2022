using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Levels
{
    [CreateAssetMenu(menuName = "Game/Settings/Level Sound Settings", fileName = "Level Sound Settings")]
    public class LevelSceneSoundSettingsSO : ScriptableObject
    {
        [SerializeField]
        private Sound _backgroundMusic;

        public Sound BackgroundMusic => _backgroundMusic;
    }
}
