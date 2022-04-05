using System;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public abstract class TriangleState : ITriangleState
    {
        public virtual void Move() { }

        public virtual void Rotate() { }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public abstract event Action<ITriangleState> ChangeState;

        public TriangleControllerCore Parent { get; set; }

        public StateFactory Factory { get; set; }

    }
}
