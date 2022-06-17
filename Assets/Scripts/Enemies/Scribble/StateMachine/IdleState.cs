using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public class IdleState : ScribbleState
    {
        public override event Action<IScribbleState> ChangeState;

        public IdleState(ScribbleControllerCore parent, StateFactory factory)
        {
            Parent = parent;
            Factory = factory;
        }
    }
}
