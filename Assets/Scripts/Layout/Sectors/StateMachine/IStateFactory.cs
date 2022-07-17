namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors.StateMachine
{
    public interface IStateFactory
    {
        State Active { get; }
        State Awaken { get; }
        State Inactive { get; }
    }
}