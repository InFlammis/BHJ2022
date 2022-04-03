using System.Collections.Generic;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies
{
    /// <summary>
    /// Configuration for the enemy type
    /// </summary>
    [CreateAssetMenu(fileName = "New Enemy InitSettings", menuName = "Game/Init Settings/Enemy InitSettings")]
    public class EnemySettings : ScriptableObject
    {
        /// <summary>
        /// Enemy type
        /// </summary>
        public EnemyType EnemyType;

        /// <summary>
        /// Max speed for the enemy
        /// </summary>
        public float MaxSpeed;

        /// <summary>
        /// Minimum magnitude of an attractive force towards the player
        /// </summary>
        public float MinAttractiveForceMagnitude;

        /// <summary>
        /// Maximum magnitude of an attractive force towards the player
        /// </summary>
        public float MaxAttractiveForceMagnitude;

        /// <summary>
        /// Minumum magnitude of a movement
        /// </summary>
        public float MinMovementMagnitude;

        /// <summary>
        /// Maximum magnitude of a movement
        /// </summary>
        public float MaxMovementMagnitude;

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

        /// <summary>
        /// Interval between two shot sequences
        /// </summary>
        public float StopFiringIntervalLength;

        /// <summary>
        /// Length of a firing sequence
        /// </summary>
        public float FiringIntervalLength;
    }
}
