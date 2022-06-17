namespace InFlammis.Victoria.Assets.Scripts.Enemies.Pawn.StateMachine
{
    /// <summary>
    /// Specialization of a IEnemyState interface into one specific for Pawn enemy type
    /// </summary>
    public interface IPawnState : IEnemyState<IPawnState>
    {
        /// <summary>
        /// Reference to the PawnControllerCore Parent object
        /// </summary>
        PawnControllerCore Parent { get; set; }

        /// <summary>
        /// Reference to the State Factory
        /// </summary>
        StateFactory Factory { get; set; }
    }
}