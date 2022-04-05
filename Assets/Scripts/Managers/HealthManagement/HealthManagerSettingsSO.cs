using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.HealthManagement
{
    [CreateAssetMenu(fileName = "New Health settings", menuName = "Game/Init Settings/Health Settings")]

    public class HealthManagerSettingsSO : ScriptableObject
    {
        /// <summary>
        /// Initial health
        /// </summary>
        public int InitHealth;

        /// <summary>
        /// Max health
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// Target to whom send events
        /// </summary>
        public string Target;

        public bool IsInvulnerable;

        /// <summary>
        /// Is the enemy invulnerable at start.
        /// </summary>
        public bool IsInvulnerableAtStart;

        /// <summary>
        /// How many seconds the enemy is invulnerable at start
        /// </summary>
        public float InvulnerableAtStartForSeconds;

    }
}
