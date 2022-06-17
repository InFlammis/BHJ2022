using System;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.ScoreManagement
{
    /// <summary>
    /// Persistently store the list of High Scores
    /// </summary>
    [CreateAssetMenu(fileName = "High score", menuName = "Score/High Score")]

    public class HighScoreRecorder : ScriptableObject
    {
        public List<Score> HighScores;
    }
}
