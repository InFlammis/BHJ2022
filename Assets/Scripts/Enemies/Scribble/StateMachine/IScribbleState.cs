namespace InFlammis.Victoria.Assets.Scripts.Enemies.Scribble.StateMachine
{
    public interface IScribbleState : IEnemyState<IScribbleState>
    {
        ScribbleControllerCore Parent { get; set; }
        StateFactory Factory { get; set; }
    }
}
