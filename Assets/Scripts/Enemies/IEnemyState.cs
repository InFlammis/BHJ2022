using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    /// <summary>
    /// Interface for a enemy state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEnemyState<T> 
        where T : IEnemyState<T>
    {
        /// <summary>
        /// Notifies of a change of state
        /// </summary>
        event Action<T> ChangeState;

        /// <summary>
        /// Move the enemy
        /// </summary>
        void Move();

        /// <summary>
        /// Rotate the enemy
        /// </summary>
        void Rotate();

        /// <summary>
        /// Invoked when the status is enabled
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Invoked when the status is disabled
        /// </summary>
        void OnExit();
    }
}
