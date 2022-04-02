using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Abstract State for an Infantry enemy
    /// </summary>
    public abstract class InfantryState : IInfantryState{
        /// <inheritdoc/>
        public virtual void Move() { }

        /// <inheritdoc/>
        public virtual void Rotate() { }

        /// <inheritdoc/>
        public virtual void OnEnter()
        {             
            //Debug.Log($"State {this.GetType().Name}: OnEnter");
        }

        /// <inheritdoc/>
        public virtual void OnExit()
        {
            //Debug.Log($"State {this.GetType().Name}: OnExit");
        }

        /// <inheritdoc/>
        public abstract event Action<IInfantryState> ChangeState;

        /// <inheritdoc/>
        public InfantryControllerCore Parent { get; set; }

        /// <inheritdoc/>
        public StateFactory Factory { get; set; }
    }
}