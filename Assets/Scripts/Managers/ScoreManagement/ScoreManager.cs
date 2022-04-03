using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.Managers.ScoreManagement
{
    /// <summary>
    /// Implementation of a ScoreManager
    /// </summary>
    public class ScoreManager : MyMonoBehaviour, IScoreManager
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        /// <summary>
        /// The current score
        /// </summary>
        private Score CurrentScore;

        /// <summary>
        /// The list of High Scores
        /// </summary>
        public HighScoreRecorder HighScores;

        private int _multiplier;
        
        /// <summary>
        /// The current Multiplier score
        /// </summary>
        public int Multiplier
        {
            get => _multiplier;
            protected set
            {
                if (value == _multiplier)
                {
                    return;
                }

                _multiplier = value;
                NotifyMultiplierValue();
            }
        }

        void Awake()
        {
            _staticObjects.Messenger.PlayerScored.AddListener(this.EnemyPlayerScored);
            _staticObjects.Messenger.GameOver.AddListener(this.LevelGameOver);
            _staticObjects.Messenger.GameStarted.AddListener(this.LevelGameStarted);
            _staticObjects.Messenger.PlayerWins.AddListener(this.LevelPlayerWins);
            _staticObjects.Messenger.MultiplierCollected.AddListener(this.PowerUpScoreMultiplierCollected);
        }

        void Start()
        {
            NotifyHighScoreValue();
            ResetCurrentScore();
            ResetMultiplier();

        }

        #region UnityEvent Handlers

        public void LevelPlayerWins(object publisher, string target)
        {
            AddToHighScore();
        }

        public void LevelGameStarted(object publisher, string target)
        {
            ResetCurrentScore();
            ResetMultiplier();
        }

        public void LevelGameOver(object publisher, string target)
        {
            AddToHighScore();
        }

        public void EnemyPlayerScored(object publisher, string target, int value)
        {
            AddToScore(value);
        }

        public void PowerUpScoreMultiplierCollected(object publisher, string target, int value)
        {
            AddToMultiplier(value);
        }

        #endregion

        #region UnityEvent Emitters

        private void NotifyHighScoreValue()
        {
            var highScore = HighScores.HighScores.OrderByDescending(x => x.Value).FirstOrDefault();
            int highScoreValue = 0;
            if (highScore != null)
            {
                highScoreValue = highScore.Value;
            }
            _staticObjects.Messenger.PublishHiScoreChanged(this, null, highScoreValue);
        }

        private void NotifyScoreValue()
        {
            _staticObjects.Messenger.PublishScoreChanged(this, null, CurrentScore.Value);
        }

        private void NotifyMultiplierValue()
        {
            _staticObjects.Messenger.PublishMultiplierChanged(this, null, _multiplier);
        }

        #endregion

        protected void AddToHighScore()
        {
            if (CurrentScore.Value == 0)
            {
                return;
            }
            CurrentScore.Date = DateTime.Now.ToString("s");
            CurrentScore.Name = "DDR";
            HighScores.HighScores.Add(CurrentScore);
            NotifyHighScoreValue();
        }

        protected void AddToScore(int score)
        {
            var totScore = score * Multiplier;
            CurrentScore.Value += totScore;
            NotifyScoreValue();
        }

        protected void AddToMultiplier(int multiplier)
        {
            Multiplier += multiplier;
        }

        protected void ResetMultiplier()
        {
            Multiplier = 1;
        }

        protected void ResetCurrentScore()
        {
            CurrentScore = new Score();
            NotifyScoreValue();
        }

        protected void ResetHighScore()
        {
            HighScores.HighScores.Clear();
            var hiScoreValue = HighScores.HighScores.OrderByDescending(x => x.Value).Select(x => x.Value).FirstOrDefault();
            _staticObjects.Messenger.HiScoreChanged.Invoke(this, null, hiScoreValue);
        }
    }
}
