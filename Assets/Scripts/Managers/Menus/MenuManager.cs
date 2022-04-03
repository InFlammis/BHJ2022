using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    /// <summary>
    /// Base class for a generic MenuManager class
    /// </summary>
    public class MenuManager : SceneManager
    {
        [SerializeField] protected MenuSceneSoundSettingsSO _soundSettings;
    }
}
