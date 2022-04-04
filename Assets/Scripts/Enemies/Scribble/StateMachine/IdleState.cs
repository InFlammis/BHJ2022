using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Scribble.StateMachine
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
