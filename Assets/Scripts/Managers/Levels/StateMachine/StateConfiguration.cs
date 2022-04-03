using BulletHellJam2022.Assets.Scripts.MessageBroker;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels.StateMachine
{
    /// <summary>
    /// Initial state configuration. Contains all reference needed to the state.
    /// </summary>
    public class StateConfiguration
    {
        public readonly IMessenger Messenger;
        /// <summary>
        /// Reference to the LevelManagerCore instance
        /// </summary>
        public readonly ILevelManagerCore LevelManagerCore;

        /// <summary>
        /// Enable the spawning of enemies
        /// </summary>
        public bool SpawnEnemiesEnabled;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="messenger"><see cref="Messenger"/></param>
        /// <param name="levelManagerCore"><see cref="ILevelManagerCore"/></param>
        /// <param name="spawnEnemiesEnabled"><see cref="SpawnEnemiesEnabled"/></param>
        public StateConfiguration(
            IMessenger messenger,
            ILevelManagerCore levelManagerCore,
            bool spawnEnemiesEnabled = true)
        {
            Messenger = messenger;
            LevelManagerCore = levelManagerCore;
            SpawnEnemiesEnabled = spawnEnemiesEnabled;
        }

    }
}
