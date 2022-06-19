using InFlammis.Victoria.Assets.Scripts.Enemies.Eye.StateMachine;
using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Eye
{
    public partial class EyeControllerCore : IEnemyControllerCore
    {
        public class IdleState : EyeState
        {
            public override event Action<IEyeState> ChangeState;

            /// <summary>
            /// Create an instance of the Idle state
            /// </summary>
            /// <param name="parent">Instance of the <see cref="EyeControllerCore"/></param>
            /// <param name="factory">Instance of the <see cref="StateFactory"/></param>
            public IdleState(EyeControllerCore parent, StateFactory factory)
            {
                Parent = parent;
                Factory = factory;
            }

        }
    }
}
