using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    [CreateAssetMenu(menuName = "Game/Init Settings/Menu Sound Settings", fileName = "MenuSoundSettings")]

    public class MenuSceneSoundSettingsSO : ScriptableObject
    {
        [SerializeField]
        private Sound _backgroundMusic;

        public Sound BackgroundMusic => _backgroundMusic;
    }
}
