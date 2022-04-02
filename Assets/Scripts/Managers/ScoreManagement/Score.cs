using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.ScoreManagement
{
    /// <summary>
    /// Models a score
    /// </summary>
    [Serializable]
    public class Score
    {
        /// <summary>
        /// Value of the score
        /// </summary>
        public int Value;

        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name;

        /// <summary>
        /// Date of the score
        /// </summary>
        public string Date;
    }
}
