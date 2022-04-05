using System;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Squiggle.StateMachine
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
