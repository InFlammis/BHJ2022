using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public class AttackTrState : StateMachineBehaviour
    {
        private static readonly string TransitionToStand = "TransitionToStand";

        [SerializeField] protected StaticObjectsSO StaticObjects;
        [SerializeField] protected TriangleInitSettings InitSettings;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private GameObject _parent;
        protected Spitter _spitter;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this._rigidbody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
            this._parent = _rigidbody.gameObject;
            this._animator = animator;
            this._spitter = this._parent.GetComponentInChildren<Spitter>();

            Debug.Log("Enter Attack");
            this._spitter.StartSpitting();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetTrigger("TransitionToStand");
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this._spitter.StopSpitting();
            Debug.Log("Exit Attack");

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
