using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.OrchestrationManagement
{
    [Serializable]
    public class EnemyTypeSettings
    {
        /// <summary>
        /// The type of enemy
        /// </summary>
        public Enemies.EnemyType EnemyTypeEnum;

        /// <summary>
        /// The enemy prefab to instantiate
        /// </summary>
        public GameObject EnemyType;

        /// <summary>
        /// Number of enemies to spawn
        /// </summary>
        public int NumToSpawn;

        /// <summary>
        /// Maximum number of this type of enemies to spawn simultaneously
        /// </summary>
        public int MaxNumOfSimultaneousSpawns;
    }
}
