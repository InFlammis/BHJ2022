using InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle.StateMachine;
using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle
{
    public partial class SquiggleControllerCore : IEnemyControllerCore
    {

        public class IdleState : SquiggleState
        {
            public override event Action<ISquiggleState> ChangeState;

            public IdleState(SquiggleControllerCore parent, StateFactory factory)
            {
                Parent = parent;
                Factory = factory;
            }
        }
    }
}
