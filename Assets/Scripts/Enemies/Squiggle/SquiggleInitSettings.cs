using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle
{
    [CreateAssetMenu(fileName = "New Squiggle Init Settings", menuName = "Game/Init Settings/Squiggle Settings")]
    public class SquiggleInitSettings : EnemySettings
    {
        private void Awake()
        {
            base.EnemyType = EnemyType.Squiggle;
        }

        /// <summary>
        /// Maximum magnitude of a movement
        /// </summary>
        public float MaxMovementMagnitude;

    }
}
