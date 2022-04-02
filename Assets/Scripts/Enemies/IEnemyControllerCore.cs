using BulletHellJam2022.Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies
{
    public interface IEnemyControllerCore
    {
        /// <summary>
        /// Reference to the Player controller core
        /// </summary>
        IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <summary>
        /// Reference to the IEnemyController parent
        /// </summary>
        IEnemyController Parent { get; }

        /// <summary>
        /// Quick reference to the Transform
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Quick reference to the Rigidbody
        /// </summary>
        Rigidbody2D Rigidbody { get; }

        /// <summary>
        /// Initial settings for the Enemy
        /// </summary>
        EnemySettings InitSettings { get; }

        /// <summary>
        /// Reference to the instance of the HealthManager for the enemy
        /// </summary>
        public IHealthManager HealthManager { get; }

        /// <summary>
        /// Method invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Move the enemy
        /// </summary>
        void Move();

        /// <summary>
        /// Handle collisions with player
        /// </summary>
        void HandleCollisionWithPlayer();
    }
}
