using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Idle state for a Pawn enemy
    /// </summary>
    public class IdleState : PawnState
    {
        /// <inheritdoc/>
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
