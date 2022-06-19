using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Player;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    public interface IEnemyControllerCore
    {
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
