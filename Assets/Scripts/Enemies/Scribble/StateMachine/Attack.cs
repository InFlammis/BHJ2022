using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public class Attack : StateMachineBehaviour
    {
        private static readonly string TransitionToSeek = "TransitionToSeek";

        [SerializeField] protected StaticObjectsSO StaticObjects;
        [SerializeField] protected EnemySettings InitSettings;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private GameObject _parent;


        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this._rigidbody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
            this._parent = _rigidbody.gameObject;
            this._animator = animator;

            var messenger = (StaticObjects.Messenger as IPlayerEventsMessenger);
            messenger.HasDied.AddListener(Player_HasDied);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Implement code that processes and affects root motion
            var playerTransform = StaticObjects.Messenger.RequestForPlayerTransform(this, null);
            if (playerTransform == null) return;

            var distance = playerTransform.position - animator.gameObject.transform.position;

            var direction = (distance).normalized;

            //add impulse - Impulse increases (clamped) with the distance from the player
            var forceMagnitude = UnityEngine.Mathf.Clamp(distance.magnitude, this.InitSettings.MinAttractiveForceMagnitude, this.InitSettings.MaxAttractiveForceMagnitude);
            var force = direction * forceMagnitude;
            this._rigidbody.AddForce(force, ForceMode2D.Impulse);

            //force movement - This effect increases when the distance lowers
            var movementMagnitude = UnityEngine.Mathf.Clamp(0.1f / distance.magnitude, this.InitSettings.MinMovementMagnitude, this.InitSettings.MaxMovementMagnitude);
            var movement = direction * movementMagnitude;
            this._rigidbody.position += new Vector2(movement.x, movement.y);

            //Clamp maximum velocity
            this._rigidbody.velocity = Vector2.ClampMagnitude(this._rigidbody.velocity, this.InitSettings.MaxSpeed);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var messenger = (StaticObjects.Messenger as IPlayerEventsMessenger);
            messenger.HasDied.RemoveAllListeners();
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{

        //}

        void Player_HasDied(object publisher, string target)
        {
            this._animator.SetTrigger(TransitionToSeek);
        }
    }
}
