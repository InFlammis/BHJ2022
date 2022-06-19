namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public class StateFactory
    {
        /// <summary>
        /// Reference to the <see cref="TriangleControllerCore"/>
        /// </summary>
        public TriangleControllerCore Parent { get; set; }

        /// <summary>
        /// Return an Attack state
        /// </summary>
        public ITriangleState AttackState { get; protected set; }

        /// <summary>
        /// Return a Seek state
        /// </summary>
        public ITriangleState SeekState { get; protected set; }

        /// <summary>
        /// Return an Idle state
        /// </summary>
        public ITriangleState IdleState { get; protected set; }

        /// <summary>
        /// Create an instance of the State Factory
        /// </summary>
        /// <param name="parent">Instance of the <see cref="TriangleControllerCore"/></param>
        public StateFactory(TriangleControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new TriangleControllerCore.AttackState(this.Parent, this);
            SeekState = new TriangleControllerCore.SeekState(this.Parent, this);
            IdleState = new TriangleControllerCore.IdleState(this.Parent, this);
        }

    }
}
