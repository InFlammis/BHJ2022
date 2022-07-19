using InFlammis.Victoria.Assets.Scripts.Enemies;
using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Weapons;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Player
{
    public class PlayerControllerCore : IPlayerControllerCore
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        public IPlayerController Parent { get; protected set; }

        public Transform Transform { get; protected set; }

        public Rigidbody2D RigidBody { get; protected set; }

        public PlayerSettings InitSettings { get; set; }

        public IHealthManager HealthManager { get; }

        public Spitter[] Weapons { get; }

        public Vector2 PlayerMovement { get; set; }
        public Vector2 PlayerRotation { get; set; }

        public bool IsGamepad { get; set; }

        public Spitter CurrentWeapon { get; set; }

        public PlayerControllerCore(IPlayerController parent)
        {
            Parent = parent;
            Transform = parent.GameObject.transform;
            RigidBody = parent.GameObject.GetComponent<Rigidbody2D>();

            HealthManager = parent.HealthManager;

            InitSettings = parent.InitSettings;
            Weapons = parent.Weapons.Select(x=>x.GetComponent<Spitter>()).ToArray();
            CurrentWeapon = Weapons[0];
        }

        public void SetPlayerMovement(Vector2 playerMovement)
        {
            PlayerMovement = playerMovement;
        }

        public void SetPlayerRotation(Vector2 playerRotation, bool isGamepad)
        {
            PlayerRotation = playerRotation;
            IsGamepad = isGamepad;
        }

        public void Move()
        {
            //var newForward = this.Parent.GameObject.transform.rotation * (new Vector3(PlayerMovement.x, PlayerMovement.y, 0));
            var newForward = new Vector3(PlayerMovement.x, PlayerMovement.y, 0);
            RigidBody.AddForce(newForward * InitSettings.ForceMultiplier, ForceMode2D.Impulse);

            //RigidBody.AddForce(PlayerMovement * InitSettings.ForceMultiplier, ForceMode2D.Impulse);
            var speed = RigidBody.velocity.magnitude;

            //Limit speed
            if (speed > InitSettings.MaxSpeed)
            {
                RigidBody.velocity = RigidBody.velocity.normalized * InitSettings.MaxSpeed;
            }

            //Stop fightship when input is zero
            if (PlayerMovement == Vector2.zero && speed != 0)
            {
                RigidBody.velocity *= InitSettings.Deceleration;
            }
        }

        public void Rotate(Vector2 mousePosition, bool isGamepad)
        {
            // GAMEPAD
            if (IsGamepad)
            {
                float angle = Mathf.Atan2(PlayerRotation.y, PlayerRotation.x) - Mathf.PI / 2;
            
                var rotationQuat = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

                var rotationMagnitude = PlayerRotation.magnitude;

                //Debug.Log($"Rotation magnitude: {rotationMagnitude}");

                if (rotationMagnitude < InitSettings.RotationTolerance)
                {
                    return;
                }

                var rotation = Quaternion.Slerp(Transform.rotation, rotationQuat, rotationMagnitude * InitSettings.RotationSpeed);
                RigidBody.SetRotation(rotation);
            }
            else
            {
                // MOUSE AND KEYBOARD
                PlayerRotation = mousePosition;

                Vector2 aimDirection = PlayerRotation - RigidBody.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                RigidBody.rotation = aimAngle;
            }      
        }

        public void StartFiring()
        {
            CurrentWeapon.StartSpitting();
        }

        public void StopFiring()
        {
            CurrentWeapon.StopSpitting();
        }

        public void FireAlt()
        {
        }

        public void OpenSelectionMenu()
        {
        }

        public void HandleCollisionWithEnemy(IEnemyController enemyController)
        {
            var damage = enemyController.InitSettings.DamageAppliedOnCollision;
            HealthManager.Damage(damage);
        }

        public void TurnLeft()
        {
            PlayerRotation = new Vector2(-1, 0);
        }

        public void TurnRight()
        {
            PlayerRotation = new Vector2(1, 0);
        }

        public void TurnUp()
        {
            PlayerRotation = new Vector2(0, 1);
        }

        public void TurnDown()
        {
            PlayerRotation = new Vector2(0, 1);
        }

        /// <summary>
        /// Execute a Rotate action on the player object, spanned on multiple frames
        /// </summary>
        /// <param name="quaternion">Rotation</param>
        /// <returns></returns>
    }
}
