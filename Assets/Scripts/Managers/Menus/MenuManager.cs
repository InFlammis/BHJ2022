using InFlammis.Victoria.Assets.Scripts.Managers.SceneManagement;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus
{
    /// <summary>
    /// Base class for a generic MenuManager class
    /// </summary>
    public class MenuManager : SceneManager
    {
        [SerializeField] protected MenuSceneSoundSettingsSO _soundSettings;
    }
}
