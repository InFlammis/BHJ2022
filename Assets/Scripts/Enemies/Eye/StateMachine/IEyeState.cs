namespace InFlammis.Victoria.Assets.Scripts.Enemies.Eye.StateMachine
{
    public interface IEyeState : IEnemyState<IEyeState>
    {
        EyeControllerCore Parent { get; set; }
        StateFactory Factory { get; set; }

    }
}
