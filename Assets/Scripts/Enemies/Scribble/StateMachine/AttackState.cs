using InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble
{
    public partial class ScribbleControllerCore
    {
        public class AttackState : ScribbleState
        {
            private Coroutine _seekPlayerCoroutine;

            public override event Action<IScribbleState> ChangeState;

            public AttackState(ScribbleControllerCore parent, StateFactory factory)
            {
                Parent = parent;
                Factory = factory;
            }

            public override void Move()
            {
                var playerTransform = Parent._messenger.RequestForPlayerTransform(this, null);
                if (playerTransform == null) return;

                var distance = playerTransform.position - Parent.Transform.position;

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
