namespace InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle.StateMachine
{
    public class StateFactory
    {
        public SquiggleControllerCore Parent { get; set; }

        public ISquiggleState AttackState { get; protected set; }

        public ISquiggleState SeekState { get; protected set; }

        public ISquiggleState IdleState { get; protected set; }

        public StateFactory(SquiggleControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new SquiggleControllerCore.AttackState(this.Parent, this);
            SeekState = new SquiggleControllerCore.SeekState(this.Parent, this);
            IdleState = new SquiggleControllerCore.IdleState(this.Parent, this);
        }

    }
}
