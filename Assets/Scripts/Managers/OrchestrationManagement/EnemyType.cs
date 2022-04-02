using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.OrchestrationManagement
{
    /// <summary>
    /// Wrapper containing a type of enemy and some settings for its spawing.
    /// </summary>
    [Serializable]
    public class EnemyType
    {
        /// <summary>
        /// Settings for the enemy type
        /// </summary>
        public EnemyTypeSettings Settings;
        /// <summary>
        /// Total number of enemies of this type to spawn
        /// </summary>
        [HideInInspector] public int TotalSpawned { get; set; }

        /// <summary>
        /// How many enemies of this type have been currently spawned
        /// </summary>
        [HideInInspector] public int CurrentlySpawned { get; set; }
    }
}
