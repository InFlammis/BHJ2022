using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    [CreateAssetMenu(menuName = "Game/Settings/Menu Sound Settings", fileName = "Menu Sound Settings")]

    public class MenuSceneSoundSettingsSO : ScriptableObject
    {
        [SerializeField]
        private Sound _backgroundMusic;

        public Sound BackgroundMusic => _backgroundMusic;
    }
}
