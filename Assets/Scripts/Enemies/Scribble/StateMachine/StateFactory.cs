namespace BulletHellJam2022.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public class StateFactory
    {
        public ScribbleControllerCore Parent { get; set; }

        public IScribbleState AttackState { get; protected set; }

        public IScribbleState SeekState { get; protected set; }

        public IScribbleState IdleState { get; protected set; }

        public StateFactory(ScribbleControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new AttackState(this.Parent, this);
            SeekState = new SeekState(this.Parent, this);
            IdleState = new IdleState(this.Parent, this);
        }
    }
}
