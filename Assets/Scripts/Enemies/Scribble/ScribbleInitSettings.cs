using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble
{
    [CreateAssetMenu(fileName = "New Scribble Init Settings", menuName = "Game/Init Settings/Scribble Settings")]
    public class ScribbleInitSettings : EnemySettings
    {
        private void Awake()
        {
            base.EnemyType = EnemyType.Scribble;
        }

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
        /// Interval between two shot sequences
        /// </summary>
        public float StopFiringIntervalLength;

        /// <summary>
        /// Length of a firing sequence
        /// </summary>
        public float FiringIntervalLength;

    }
}
