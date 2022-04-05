using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public class AttackState : TriangleState
    {
        private Coroutine _seekPlayerCoroutine;

        public override void Move()
        {
            var mag = UnityEngine.Random.value * Parent.InitSettings.MaxMovementMagnitude;
            var impulse = UnityEngine.Random.insideUnitCircle * mag;

            Parent.Rigidbody.AddForce(impulse);
        }

        public override void Rotate() { }

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

        public override void OnExit()
        {
            base.OnExit();
            Parent.Parent.StopCoroutine(_seekPlayerCoroutine);
        }

        /// <summary>
        /// Create an instance of Attack state
        /// </summary>
        /// <param name="parent">Instance of <see cref="TriangleControllerCore"/></param>
        /// <param name="factory">Instance of <see cref="StateFactory"/></param>
        public AttackState(TriangleControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }

        public override event Action<ITriangleState> ChangeState;

    }
}
