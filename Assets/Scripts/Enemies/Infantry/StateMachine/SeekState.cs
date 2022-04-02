using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Seek state for an Infantry enemy
    /// </summary>
    public class SeekState : InfantryState
    {
        /// <inheritdoc/>
        public override event Action<IInfantryState> ChangeState;

        /// <inheritdoc/>
        public override void Move()
        {
            var mag = UnityEngine.Random.value * Parent.InitSettings.MaxMovementMagnitude;
            var impulse = UnityEngine.Random.insideUnitCircle * mag;

            Parent.Rigidbody.AddForce(impulse);
        }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();
            Parent.Parent.StartCoroutine(SeekPlayer());
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();
            Parent.Parent.StopCoroutine(SeekPlayer());
        }

        /// <summary>
        /// Check that the player is alive or dead and if alive, invoke a state change
        /// </summary>
        /// <returns></returns>
        private IEnumerator SeekPlayer()
        {
            while (true)
            {
                yield return new WaitWhile(() => Parent.PlayerControllerCore.HealthManager.IsDead);
                //Player found
                ChangeState?.Invoke(Factory.AttackState);
                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// Create an instance of Seek state
        /// </summary>
        /// <param name="parent">Instance of <see cref="InfantryControllerCore"/></param>
        /// <param name="factory">Instance of <see cref="StateFactory"/></param>
        public SeekState(InfantryControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }
    }
}
