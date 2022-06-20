using InFlammis.Victoria.Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Eye.StateMachine
{
    public class EyeSeek : StateMachineBehaviour
    {
        private static readonly string TransitionToAttack = "TransitionToAttack";

        [SerializeField] protected StaticObjectsSO StaticObjects;
        [SerializeField] protected EyeInitSettings InitSettings;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private GameObject _parent;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this._rigidbody = animator.gameObject.GetComponentInParent<Rigidbody2D>();
            this._parent = _rigidbody.gameObject;
            this._animator = animator;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (StaticObjects.Messenger.RequestForPlayerIsAlive(this, null))
            {
                animator.SetTrigger(TransitionToAttack);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var mag = UnityEngine.Random.value * InitSettings.MaxMovementMagnitude;
            var impulse = UnityEngine.Random.insideUnitCircle * mag;

            this._rigidbody.AddForce(impulse);
        }
    }
}
