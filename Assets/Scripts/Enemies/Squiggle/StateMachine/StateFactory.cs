namespace BulletHellJam2022.Assets.Scripts.Enemies.Squiggle.StateMachine
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
            AttackState = new AttackState(this.Parent, this);
            SeekState = new SeekState(this.Parent, this);
            IdleState = new IdleState(this.Parent, this);
        }

    }
}
