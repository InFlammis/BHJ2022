using System;
using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public class SeekState : TriangleState
    {
        public override void Move()
        {
            var mag = UnityEngine.Random.value * Parent.InitSettings.MaxMovementMagnitude;
            var impulse = UnityEngine.Random.insideUnitCircle * mag;

            Parent.Rigidbody.AddForce(impulse);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Parent.Parent.StartCoroutine(SeekPlayer());
        }

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
        /// <param name="parent">Instance of <see cref="TriangleControllerCore"/></param>
        /// <param name="factory">Instance of <see cref="StateFactory"/></param>
        public SeekState(TriangleControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }

        public override event Action<ITriangleState> ChangeState;

    }
}
