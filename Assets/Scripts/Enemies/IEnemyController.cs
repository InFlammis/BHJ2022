using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Weapons;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    /// <summary>
    /// Interface for an IEnemyController
    /// </summary>
    public interface IEnemyController : IMyMonoBehaviour
    {
        public StaticObjectsSO StaticObjects { get; }

        public string Target { get; }

        /// <summary>
        /// Instance of EnemySettings
        /// </summary>
        EnemySettings InitSettings { get; }

        /// <summary>
        /// Instance of the game object for the enemy
        /// </summary>
        //GameObject GameObject { get; }

        /// <summary>
        /// Core class for the EnemyController
        /// </summary>
        IEnemyControllerCore Core { get; set; }

        /// <summary>
        /// Instance of the HealthManager
        /// </summary>
        IHealthManager HealthManager { get; set; }

        /// <summary>
        /// Collection of available spitters
        /// </summary>
        Spitter[] Spitters { get; set; }
    }
}