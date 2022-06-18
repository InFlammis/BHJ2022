using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public abstract class ScribbleState : IScribbleState
    {
        public ScribbleControllerCore Parent { get; set; }

        public StateFactory Factory { get; set; }

        public abstract event Action<IScribbleState> ChangeState;

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
