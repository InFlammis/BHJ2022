using InFlammis.Victoria.Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public class DodgeTrState : StateMachineBehaviour
    {
        private static readonly string TransitionToSpin = "TransitionToSpin";

        [SerializeField] protected StaticObjectsSO StaticObjects;
        [SerializeField] protected TriangleInitSettings InitSettings;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private GameObject _parent;

        private Vector2 _dodgeDirection = Vector2.zero;
        private float _dodgeForceMagnitude = 20f;
        private bool isDodging = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this._rigidbody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
            this._parent = _rigidbody.gameObject;
            this._animator = animator;

            Debug.Log("Enter Dodge");

            isDodging = false;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0;


            var playerTransform = StaticObjects.Messenger.RequestForPlayerTransform(this, null);
            if (playerTransform != null)
            {
                _dodgeDirection = CalculateDodgeDirection(playerTransform);
                _rigidbody.AddForce(_dodgeDirection * _dodgeForceMagnitude, ForceMode2D.Impulse);

            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //if (!isDodging)
            //{
            //    _rigidbody.AddForce(_dodgeDirection * _dodgeForceMagnitude, ForceMode2D.Impulse);
            //    isDodging = true;
            //}
        }

        private Vector2 CalculateDodgeDirection(Transform playerTransform)
        {
            var diff = (_parent.transform.position - playerTransform.position).normalized;

            var playerFront = playerTransform.up;

            var perp = Vector2.Perpendicular(diff);

            var cross = Vector3.Cross(diff, playerFront);

            var dot = Vector3.Dot(cross, Vector3.forward);

            var direction = Mathf.Sign(dot);

            return -direction * perp;
        }


        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Exit Dodge");

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
