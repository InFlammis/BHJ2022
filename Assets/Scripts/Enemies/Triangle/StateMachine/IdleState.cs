using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public class IdleState : TriangleState
    {
        public override event Action<ITriangleState> ChangeState;

        /// <summary>
        /// Create an instance of the Idle state
        /// </summary>
        /// <param name="parent">Instance of the <see cref="TriangleControllerCore"/></param>
        /// <param name="factory">Instance of the <see cref="StateFactory"/></param>
        public IdleState(TriangleControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }

    }
}
