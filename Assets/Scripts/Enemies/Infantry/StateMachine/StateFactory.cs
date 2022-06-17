namespace InFlammis.Victoria.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Factory that builds all possible states for an Infantry enemy type
    /// </summary>
    public class StateFactory
    {
        /// <summary>
        /// Reference to the <see cref="InfantryControllerCore"/>
        /// </summary>
        public InfantryControllerCore Parent { get; set; }
        
        /// <summary>
        /// Return an Attack state
        /// </summary>
        public IInfantryState AttackState { get; protected set; }
        
        /// <summary>
        /// Return a Seek state
        /// </summary>
        public IInfantryState SeekState { get; protected set; }
        
        /// <summary>
        /// Return an Idle state
        /// </summary>
        public IInfantryState IdleState { get; protected set; }

        /// <summary>
        /// Create an instance of the State Factory
        /// </summary>
        /// <param name="parent">Instance of the <see cref="InfantryControllerCore"/></param>
        public StateFactory(InfantryControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new AttackState(this.Parent, this);
            SeekState = new SeekState(this.Parent, this);
            IdleState = new IdleState(this.Parent, this);
        }
    }
}
