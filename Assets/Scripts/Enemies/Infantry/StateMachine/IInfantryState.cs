using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Specialization of a IEnemyState interface into one specific for Infantry enemy type
    /// </summary>
    public interface IInfantryState : IEnemyState<IInfantryState>
    {
        /// <summary>
        /// Reference to the InfantryControllerCore Parent object
        /// </summary>
        InfantryControllerCore Parent { get; set; }

        /// <summary>
        /// Reference to the State Factory
        /// </summary>
        StateFactory Factory { get; set; }
    }
}
