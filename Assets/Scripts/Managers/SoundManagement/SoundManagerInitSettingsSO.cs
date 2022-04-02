using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.SoundManagement
{
    [CreateAssetMenu(menuName = "Game/Init Settings/SoundManager Init Settings", fileName = "SoundManagerInitSettings")]
    public class SoundManagerInitSettingsSO : ScriptableObject
    {
        public int AudioSourcePoolSize = 10;
        public AudioClip MenuMusic;
        public AudioClip GameMusic;
    }
}
