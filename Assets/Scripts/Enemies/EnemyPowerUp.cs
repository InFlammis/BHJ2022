using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    /// <summary>
    /// Base class modeling a power-up
    /// </summary>
    [Serializable]
    public class EnemyPowerUp
    {
        /// <summary>
        /// Instance of a PowerUp GameObject
        /// </summary>
        [RequiredMember]
        public GameObject PowerUp;

        /// <summary>
        /// Probability this power up is spawned on enemy's destruction.
        /// </summary>
        [Range(0, 1)]
        public float ReleaseRate;
    }
}
