using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.OrchestrationManagement
{
    [CreateAssetMenu(menuName = "Game/Settings/OrchestrationManager Settings", fileName = "OrchestrationManager Settings")]
    public class OrchestrationManagerSettingsSO : ScriptableObject
    {
        public bool IsIdle = false;
        public float DelayBetweenWaves = 1.0f;
        public float DelayBeforeStart = 1.0f;
        public float DelayAfterEnd = 1.0f;

        public Wave[] Waves;

    }
}
