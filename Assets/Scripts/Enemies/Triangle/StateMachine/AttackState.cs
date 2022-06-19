using InFlammis.Victoria.Assets.Scripts.Enemies.Triangle.StateMachine;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle
{
    public partial class TriangleControllerCore : IEnemyControllerCore
    {

        public class AttackState : TriangleState
        {
            private Coroutine _seekPlayerCoroutine;

            public override event Action<ITriangleState> ChangeState;

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

                var messenger = (Parent._messenger as IPlayerEventsMessenger);
                messenger.HasDied.AddListener(Player_HasDied);
            }

            void Player_HasDied(object publisher, string target)
            {
                ChangeState?.Invoke(Factory.IdleState);
            }


            public override void OnExit()
            {
                base.OnExit();

                var messenger = (Parent._messenger as IPlayerEventsMessenger);
                messenger.HasDied.RemoveAllListeners();
            }
        }
    }
}
