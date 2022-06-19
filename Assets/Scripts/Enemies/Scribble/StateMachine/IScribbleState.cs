using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine
{
    [Obsolete]
    public interface IScribbleState : IEnemyState<IScribbleState>
    {
        ScribbleControllerCore Parent { get; set; }
        StateFactory Factory { get; set; }
    }
}
