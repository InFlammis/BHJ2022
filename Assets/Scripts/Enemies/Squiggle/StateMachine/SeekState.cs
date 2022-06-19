using InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle.StateMachine;
using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle
{
    public partial class SquiggleControllerCore : IEnemyControllerCore
    {

        public class SeekState : SquiggleState
        {
            public override event Action<ISquiggleState> ChangeState;

            public SeekState(SquiggleControllerCore parent, StateFactory factory)
            {
                Parent = parent;
                Factory = factory;
            }

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
                    yield return new WaitUntil(() => Parent._messenger.RequestForPlayerIsAlive(this, null));
                    //Player found
                    ChangeState?.Invoke(Factory.AttackState);
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
}
