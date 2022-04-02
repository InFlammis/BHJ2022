using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Attack state for an Infantry enemy
    /// </summary>
    public class AttackState : InfantryState
    {
        private Coroutine _fireCoroutine;
        private Coroutine _seekPlayerCoroutine;
        private bool fireCondition = false;


        /// <inheritdoc/>
        public override void Move()
        {
            var mag = UnityEngine.Random.value * Parent.InitSettings.MaxMovementMagnitude;
            var impulse = UnityEngine.Random.insideUnitCircle * mag;

            Parent.Rigidbody.AddForce(impulse);
        }

        /// <inheritdoc/>
        public override void Rotate()
        {
            LookAtPlayer();
        }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();
            fireCondition = true;
            _seekPlayerCoroutine = Parent.Parent.StartCoroutine(SeekPlayer());
            _fireCoroutine = Parent.Parent.StartCoroutine(Fire());
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();
            fireCondition = false;
            Parent.Parent.StopCoroutine(_seekPlayerCoroutine);
            Parent.Parent.StopCoroutine(_fireCoroutine);
            Parent.CurrentWeapon.StopFiring();

        }

        /// <summary>
        /// Rotate the enemy object so as to face the player object
        /// </summary>
        protected void LookAtPlayer()
        {
            if (Parent.PlayerControllerCore.Transform == null )
            {
                return;
            }

            float rotationSpeed = 0.1f;

            //Improve the aim of the enemy implementing this suggestion
            //https://stackoverflow.com/questions/3211374/2d-game-algorithm-to-calculate-a-bullets-needed-speed-to-hit-target

            var playerDirection = (Parent.PlayerControllerCore.Transform.position - Parent.Transform.position);

            float angle = (Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg) - 90;
            var rotation = Quaternion.Euler(0, 0, angle);

            Parent.Transform.rotation = Quaternion.Slerp(Parent.Transform.rotation, rotation, rotationSpeed);

        }

        /// <inheritdoc/>
        public IEnumerator Fire()
        {
            var stopFiringInterval = 1.0f;
            var firingInterval = 1.0f;

            while (fireCondition)
            {
                var startFiringAt = Time.fixedTime;
                Parent.CurrentWeapon.StartFiring();
                yield return new WaitWhile(() => startFiringAt + Parent.InitSettings.FiringIntervalLength > Time.fixedTime);

                var stopFiringAt = Time.fixedTime;
                Parent.CurrentWeapon.StopFiring();
                yield return new WaitWhile(() => stopFiringAt + Parent.InitSettings.StopFiringIntervalLength > Time.fixedTime);

            }
        }

        /// <summary>
        /// Check that the player is alive or dead and if dead, invoke a state change
        /// </summary>
        /// <returns></returns>
        public IEnumerator SeekPlayer()
        {
            while (true)
            {
                yield return new WaitUntil(() => Parent.PlayerControllerCore.HealthManager.IsDead);
                //Player found
                ChangeState?.Invoke(Factory.SeekState);
                yield return new WaitForSeconds(1);
            }
        }

        /// <inheritdoc/>
        public override event Action<IInfantryState> ChangeState;

        /// <summary>
        /// Create an instance of Attack state
        /// </summary>
        /// <param name="parent">Instance of <see cref="InfantryControllerCore"/></param>
        /// <param name="factory">Instance of <see cref="StateFactory"/></param>
        public AttackState(InfantryControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }
    }
}
