using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.SoundManagement
{
    [CreateAssetMenu(menuName = "Game/Settings/SoundManager Settings", fileName = "SoundManager Settings")]
    public class SoundManagerInitSettingsSO : ScriptableObject
    {
        public int AudioSourcePoolSize = 10;
        public AudioClip MenuMusic;
        public AudioClip GameMusic;
    }
}
