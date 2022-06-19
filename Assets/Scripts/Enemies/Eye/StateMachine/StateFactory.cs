namespace InFlammis.Victoria.Assets.Scripts.Enemies.Eye.StateMachine
{
    public class StateFactory
    {
        /// <summary>
        /// Reference to the <see cref="EyeControllerCore"/>
        /// </summary>
        public EyeControllerCore Parent { get; set; }

        /// <summary>
        /// Return an Attack state
        /// </summary>
        public IEyeState AttackState { get; protected set; }

        /// <summary>
        /// Return a Seek state
        /// </summary>
        public IEyeState SeekState { get; protected set; }

        /// <summary>
        /// Return an Idle state
        /// </summary>
        public IEyeState IdleState { get; protected set; }

        /// <summary>
        /// Create an instance of the State Factory
        /// </summary>
        /// <param name="parent">Instance of the <see cref="EyeControllerCore"/></param>
        public StateFactory(EyeControllerCore parent)
        {
            this.Parent = parent;
            AttackState = new EyeControllerCore.AttackState(this.Parent, this);
            SeekState = new EyeControllerCore.SeekState(this.Parent, this);
            IdleState = new EyeControllerCore.IdleState(this.Parent, this);
        }

    }
}
