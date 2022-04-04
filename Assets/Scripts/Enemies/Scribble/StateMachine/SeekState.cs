using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public class SeekState : ScribbleState
    {
        public override event Action<IScribbleState> ChangeState;

        public SeekState(ScribbleControllerCore parent, StateFactory factory)
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
                yield return new WaitWhile(() => Parent.PlayerControllerCore.HealthManager.IsDead);
                //Player found
                ChangeState?.Invoke(Factory.AttackState);
                yield return new WaitForFixedUpdate();
            }
        }

    }
}
