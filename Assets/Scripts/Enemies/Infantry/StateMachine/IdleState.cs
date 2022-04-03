using System;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Idle state for an Infantry enemy
    /// </summary>
    public class IdleState : InfantryState
    {
        /// <inheritdoc/>
        public override event Action<IInfantryState> ChangeState;

        /// <summary>
        /// Create an instance of the Idle state
        /// </summary>
        /// <param name="parent">Instance of the <see cref="InfantryControllerCore"/></param>
        /// <param name="factory">Instance of the <see cref="StateFactory"/></param>
        public IdleState(InfantryControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }
    }
}
