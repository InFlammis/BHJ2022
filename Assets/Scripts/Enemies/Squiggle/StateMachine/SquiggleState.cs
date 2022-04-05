using System;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Squiggle.StateMachine
{
    public abstract class SquiggleState : ISquiggleState
    {
        public SquiggleControllerCore Parent { get; set; }

        public StateFactory Factory { get; set; }

        public abstract event Action<ISquiggleState> ChangeState;

        public virtual void Move()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void Rotate()
        {
        }

    }
}
