using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine
{
    /// <summary>
    /// Generic abstract state for a Level
    /// </summary>
    public abstract class State
    {
        public readonly StateConfiguration Configuration;

        /// <summary>
        /// Gets or sets the delay in seconds during a change of state.
        /// </summary>
        public virtual float ChangeStateDelay { get; set; } = 1;

        public State(StateConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Invoked when the state enters..
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// Invoked when the state exits.
        /// </summary>
        public virtual void OnExit()
        {
        }
    }
}
