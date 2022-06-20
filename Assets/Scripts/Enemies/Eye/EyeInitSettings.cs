using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Eye
{
    [CreateAssetMenu(fileName = "New Eye Init Settings", menuName = "Game/Init Settings/Eye Settings")]
    public class EyeInitSettings : EnemySettings
    {
        private void Awake()
        {
            base.EnemyType = EnemyType.Eye;
        }

        /// <summary>
        /// Maximum magnitude of a movement
        /// </summary>
        public float MaxMovementMagnitude;

    }
}
