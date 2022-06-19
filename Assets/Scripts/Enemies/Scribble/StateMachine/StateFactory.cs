using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine
{
    [Obsolete]
    public class StateFactory
    {
        public ScribbleControllerCore Parent { get; set; }

        public IScribbleState AttackState { get; protected set; }

        public IScribbleState SeekState { get; protected set; }

        public IScribbleState IdleState { get; protected set; }

        public StateFactory(ScribbleControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new ScribbleControllerCore.AttackState(this.Parent, this);
            SeekState = new ScribbleControllerCore.SeekState(this.Parent, this);
            IdleState = new ScribbleControllerCore.IdleState(this.Parent, this);
        }
    }
}
