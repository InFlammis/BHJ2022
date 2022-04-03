using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Abstract State for a Pawn enemy
    /// </summary>
    public abstract class PawnState : IPawnState
    {
        public virtual void Move() { }

        public virtual void Rotate() { }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public abstract event Action<IPawnState> ChangeState;

        public PawnControllerCore Parent { get; set; }

        public StateFactory Factory { get; set; }
    }
}