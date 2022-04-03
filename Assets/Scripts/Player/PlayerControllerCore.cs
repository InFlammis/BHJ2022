using BulletHellJam2022.Assets.Scripts.Enemies;
using BulletHellJam2022.Assets.Scripts.Managers;
using BulletHellJam2022.Assets.Scripts.Managers.HealthManagement;
using BulletHellJam2022.Assets.Scripts.Weapons;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Player
{
    public class PlayerControllerCore : IPlayerControllerCore
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        public IPlayerController Parent { get; protected set; }

        public Transform Transform { get; protected set; }

        public Rigidbody2D RigidBody { get; protected set; }

        public PlayerSettings InitSettings { get; set; }

        public IHealthManager HealthManager { get; }

        public WeaponBase[] Weapons { get; }

        public Vector2 PlayerInput { get; set; }

        public WeaponBase CurrentWeapon { get; set; }

        public PlayerControllerCore(IPlayerController parent)
        {
            Parent = parent;
            Transform = parent.GameObject.transform;
            RigidBody = parent.GameObject.GetComponent<Rigidbody2D>();

            HealthManager = parent.HealthManager;

            InitSettings = parent.InitSettings;
            Weapons = parent.Weapons.Select(x=>x.GetComponent<WeaponBase>()).ToArray();
            CurrentWeapon = Weapons[0];
        }

        public void SetPlayerInput(Vector2 playerInput)
        {
            PlayerInput = playerInput;
        }

        public void Move()
        {
            RigidBody.AddForce(PlayerInput * InitSettings.ForceMultiplier, ForceMode2D.Impulse);
            var speed = RigidBody.velocity.magnitude;

            //Limit speed
            if (speed > InitSettings.MaxSpeed)
            {
                RigidBody.velocity = RigidBody.velocity.normalized * InitSettings.MaxSpeed;
            }

            //Stop fightship when input is zero
            if (PlayerInput == Vector2.zero && speed != 0)
            {
                RigidBody.velocity *= InitSettings.Deceleration;
            }
        }

        public void Rotate(Vector2 inputVector)
        {
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) - Mathf.PI / 2;
            
            var rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        public void StartFiring()
        {
            CurrentWeapon.StartFiring();
        }

        public void StopFiring()
        {
            CurrentWeapon.StopFiring();
        }

        public void FireAlt()
        {
        }

        public void OpenSelectionMenu()
        {
        }

        public void HandleCollisionWithEnemy(IEnemyControllerCore enemyController)
        {
            var damage = enemyController.InitSettings.DamageAppliedOnCollision;
            HealthManager.Damage(damage);
        }

        public void TurnLeft()
        {
            var rotation = Quaternion.Euler(0, 0, 90);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        public void TurnRight()
        {
            var rotation = Quaternion.Euler(0, 0, -90);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        public void TurnUp()
        {
            var rotation = Quaternion.Euler(0, 0, 0);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        public void TurnDown()
        {
            var rotation = Quaternion.Euler(0, 0, 180);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        /// <summary>
        /// Execute a Rotate action on the player object, spanned on multiple frames
        /// </summary>
        /// <param name="quaternion">Rotation</param>
        /// <returns></returns>
        private IEnumerator DoRotatePlayer(Quaternion quaternion)
        {
            float tolerance = 0.95f;
            float rotationSpeed = 0.01f;

            while ( Mathf.Abs(Quaternion.Dot(Transform.rotation, quaternion) ) < tolerance)
            {
                Transform.rotation = Quaternion.Slerp(Transform.rotation, quaternion, rotationSpeed);
                yield return  null;
            }

            Transform.rotation = quaternion;
        }
    }
}
