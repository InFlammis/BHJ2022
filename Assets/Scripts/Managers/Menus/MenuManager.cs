using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
