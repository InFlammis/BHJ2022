﻿using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.OrchestrationManagement
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
