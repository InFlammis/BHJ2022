using System;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Eye.StateMachine
{
    public abstract class EyeState : IEyeState
    {
        public virtual void Move() { }

        public virtual void Rotate() { }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public abstract event Action<IEyeState> ChangeState;

        public EyeControllerCore Parent { get; set; }

        public StateFactory Factory { get; set; }

    }
}
