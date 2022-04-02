using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Attack state for a Pawn enemy
    /// </summary>
    public class AttackState : PawnState
    {
        private Coroutine _seekPlayerCoroutine;

        /// <inheritdoc/>
        public override void Move()
        {
            if (Parent.PlayerControllerCore == null || Parent.PlayerControllerCore.Transform == null) return;

            var distance = Parent.PlayerControllerCore.Transform.position - Parent.Transform.position;

            var direction = (distance).normalized;

            //add impulse - Impulse increases (clamped) with the distance from the player
            var forceMagnitude = UnityEngine.Mathf.Clamp(distance.magnitude, Parent.InitSettings.MinAttractiveForceMagnitude, Parent.InitSettings.MaxAttractiveForceMagnitude);
            var force = direction * forceMagnitude;
            Parent.Rigidbody.AddForce(force, ForceMode2D.Impulse);

            //force movement - This effect increases when the distance lowers
            var movementMagnitude = UnityEngine.Mathf.Clamp(0.1f / distance.magnitude, Parent.InitSettings.MinMovementMagnitude, Parent.InitSettings.MaxMovementMagnitude);
            var movement = direction * movementMagnitude;
            Parent.Rigidbody.position += new Vector2(movement.x, movement.y);

            //Clamp maximum velocity
            Parent.Rigidbody.velocity = Vector2.ClampMagnitude(Parent.Rigidbody.velocity, Parent.InitSettings.MaxSpeed);
        }

        /// <inheritdoc/>
        public override void Rotate() { }

        /// <inheritdoc/>
        public override void OnEnter()
        {
            base.OnEnter();
            _seekPlayerCoroutine = Parent.Parent.StartCoroutine(SeekPlayer());
        }
        
        /// <summary>
        /// Check that the player is alive or dead and if dead, invoke a state change
        /// </summary>
        /// <returns></returns>
        private IEnumerator SeekPlayer()
        {
            while (true)
            {
                yield return new WaitUntil(() => Parent.PlayerControllerCore.HealthManager.IsDead);
                //Player found
                ChangeState?.Invoke(Factory.AttackState);
                yield return new WaitForFixedUpdate();
            }
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            base.OnExit();
            Parent.Parent.StopCoroutine(_seekPlayerCoroutine);
        }

        /// <summary>
        /// Create an instance of Attack state
        /// </summary>
        /// <param name="parent">Instance of <see cref="PawnControllerCore"/></param>
        /// <param name="factory">Instance of <see cref="StateFactory"/></param>
        public AttackState(PawnControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }

        /// <inheritdoc/>
        public override event Action<IPawnState> ChangeState;
    }
}
