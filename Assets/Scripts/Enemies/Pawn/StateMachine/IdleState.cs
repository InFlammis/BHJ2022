using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Idle state for a Pawn enemy
    /// </summary>
    public class IdleState : PawnState
    {
        public override event Action<IPawnState> ChangeState;

        /// <summary>
        /// Create an instance of the Idle state
        /// </summary>
        /// <param name="parent">Instance of the <see cref="PawnControllerCore"/></param>
        /// <param name="factory">Instance of the <see cref="StateFactory"/></param>
        public IdleState(PawnControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }
    }
}
