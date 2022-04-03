namespace BulletHellJam2022.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Factory that builds all possible states for an Pawn enemy type
    /// </summary>
    public class StateFactory
    {
        /// <summary>
        /// Reference to the <see cref="PawnControllerCore"/>
        /// </summary>
        public PawnControllerCore Parent { get; set; }
        
        /// <summary>
        /// Return an Attack state
        /// </summary>
        public IPawnState AttackState { get; protected set; }

        /// <summary>
        /// Return a Seek state
        /// </summary>
        public IPawnState SeekState { get; protected set; }

        /// <summary>
        /// Return an Idle state
        /// </summary>
        public IPawnState IdleState { get; protected set; }

        /// <summary>
        /// Create an instance of the State Factory
        /// </summary>
        /// <param name="parent">Instance of the <see cref="PawnControllerCore"/></param>
        public StateFactory(PawnControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new AttackState(this.Parent, this);
            SeekState = new SeekState(this.Parent, this);
            IdleState = new IdleState(this.Parent, this);
        }
    }

}
