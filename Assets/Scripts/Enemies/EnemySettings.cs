using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    /// <summary>
    /// Configuration for the enemy type
    /// </summary>
    [CreateAssetMenu(fileName = "New Enemy Settings", menuName = "Game/Init Settings/Enemy Settings")]
    public class EnemySettings : ScriptableObject
    {
        /// <summary>
        /// Enemy type
        /// </summary>
        public EnemyType EnemyType;

        /// <summary>
        /// Damage applied to the player on direct collision
        /// </summary>
        public int DamageAppliedOnCollision;

        /// <summary>
        /// List of powerups owned by this type of enemy
        /// </summary>
        public List<EnemyPowerUp> Powerups;

        /// <summary>
        /// Score attributed to the player on enemy's destruction
        /// </summary>
        public int PlayerScoreWhenKilled;
    }
}
