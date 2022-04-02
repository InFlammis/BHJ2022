using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Abstract State for a Pawn enemy
    /// </summary>
    public abstract class PawnState : IPawnState
    {
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
        public abstract event Action<IPawnState> ChangeState;

        /// <inheritdoc/>
        public PawnControllerCore Parent { get; set; }

        /// <inheritdoc/>
        public StateFactory Factory { get; set; }
    }
}