namespace InFlammis.Victoria.Assets.Scripts.Enemies.Squiggle.StateMachine
{
    public interface ISquiggleState : IEnemyState<ISquiggleState>
    {
        SquiggleControllerCore Parent { get; set; }
        StateFactory Factory { get; set; }

    }
}
