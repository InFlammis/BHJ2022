using InFlammis.Victoria.Assets.Scripts.Enemies;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Player
{
    /// <summary>
    /// Exposes all the functionalities required by a PlayerControllerCore
    /// </summary>
    public interface IPlayerControllerCore
    {
        /// <summary>
        /// Reference to the IPlayerController parent instance
        /// </summary>
        IPlayerController Parent { get; }

        /// <summary>
        /// Quick reference to the Transform
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Quick reference to the RigidBody
        /// </summary>
        Rigidbody2D RigidBody { get; }

        /// <summary>
        /// Reference to the HealthManager instance
        /// </summary>
        public IHealthManager HealthManager { get; }

        /// <summary>
        /// Initial configuration for the player
        /// </summary>
        public PlayerSettings InitSettings { get; set; }

        /// <summary>
        /// private instance that stores the last player input
        /// </summary>
        Vector2 PlayerMovement { get; set; }

        /// <summary>
        /// Set the Player Input variable
        /// </summary>
        /// <param name="playerInput"></param>
        void SetPlayerMovement(Vector2 playerInput);

        /// <summary>
        /// Move the player
        /// </summary>
        void Move();

        void Rotate();


        /// <summary>
        /// Start a firing action lasting across multiple frames
        /// </summary>
        void StartFiring();

        /// <summary>
        /// Stop a firing action lasting across multiple frames
        /// </summary>
        void StopFiring();

        /// <summary>
        /// Alternate fire
        /// </summary>
        void FireAlt();

        /// <summary>
        /// Open the selection menu
        /// </summary>
        void OpenSelectionMenu();

        /// <summary>
        /// EventHandler invoked when the player collides with an enemy
        /// </summary>
        /// <param name="enemyController">Enemy which the player is colliding with</param>
        void HandleCollisionWithEnemy(IEnemyControllerCore enemyController);
        void SetPlayerRotation(Vector2 inputVector);

        /// <summary>
        /// Turn left
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Turn right
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Turn up
        /// </summary>
        void TurnUp();

        /// <summary>
        /// Turn Down
        /// </summary>
        void TurnDown();
    }
}