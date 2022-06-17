using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.LogManagement
{
    [CreateAssetMenu(menuName = "Game/Settings/LogManager Settings", fileName = "LogManager Settings")]
    public class LogManagerSettingsSO : ScriptableObject
    {
        public bool _logEnabled = false;
        public Level _filterLevel = Level.Assert;
    }
}
