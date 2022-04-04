using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHellJam2022.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public interface IScribbleState : IEnemyState<IScribbleState>
    {
        ScribbleControllerCore Parent { get; set; }
        StateFactory Factory { get; set; }

    }
}
