namespace BulletHellJam2022.Assets.Scripts.Managers.ScoreManagement
{
    /// <summary>
    /// Interface for a score manager.
    /// A score manager is a component that keeps track of the current player's score
    /// and the top high-scores.
    /// </summary>
    public interface IScoreManager : IMyMonoBehaviour
    {
        void LevelPlayerWins(object publisher, string target);

        void LevelGameStarted(object publisher, string target);

        void LevelGameOver(object publisher, string target);

        void EnemyPlayerScored(object publisher, string target, int value);

        void PowerUpScoreMultiplierCollected(object publisher, string target, int value);

    }
}
