using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.LogManagement
{
    public interface ILogManager
    {
    }

    public enum Level
    {
        Assert,
        Debug,
        Information,
        Event,
        Warning,
        Exception,
        Error,
        Panic
    }

    public class Logger :
        MonoBehaviour,
        ILogManager
    {
        [SerializeField] private bool _logEnabled;
        [SerializeField] private Level _filterLevel;

        [SerializeField] private StaticObjectsSO _staticObjects;

        private IMessenger _messenger => _staticObjects.Messenger;

        public bool logEnabled => _logEnabled;
        public Level filterLevel => _filterLevel;

        void Awake()
        {
            _messenger.GameOver.AddListener(LevelGameOver);
            _messenger.GameStarted.AddListener(LevelGameStarted);
            _messenger.PlayerScored.AddListener(PlayerScored);
            _messenger.PlayerWins.AddListener(PlayerWins);
            _messenger.OrchestrationStarted.AddListener(OrchestrationStarted);
            _messenger.OrchestrationCancelled.AddListener(OrchestrationCancelled);
            _messenger.OrchestrationComplete.AddListener(OrchestrationComplete);
            _messenger.ScoreChanged.AddListener(ScoreChanged);
            _messenger.ScoreChanged.AddListener(MultiplierChanged);
            _messenger.HiScoreChanged.AddListener(HighScoreChanged);
            (_messenger as IPlayerEventsMessenger).HasDied.AddListener(PlayerHasDied);
            (_messenger as IEnemyEventsMessenger).HasDied.AddListener(EnemyHasDied);
        }


        public bool IsLogTypeAllowed(Level logType)
        {
            return logType >= filterLevel;
        }

        public void Log(Level level, object message)
        {
            if (logEnabled && IsLogTypeAllowed(level))
            {
                Debug.Log(message);
            }
        }

        public void Log(Level level, object message, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(level))
            {
                Debug.Log(message, context);
            }
        }

        public void LogError(string tag, object message)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Error))
            {
                Debug.LogError(message);
            }
        }

        public void LogError(string tag, object message, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Error))
            {
                Debug.LogError(message, context);
            }
        }

        public void LogException(Exception exception)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Exception))
            {
                Debug.LogException(exception);
            }
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Exception))
            {
                Debug.LogException(exception, context);
            }
        }

        public void LogWarning(string tag, object message)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Warning))
            {
                Debug.LogWarning(message);
            }
        }

        public void LogWarning(string tag, object message, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Warning))
            {
                Debug.LogWarning(message, context);
            }
        }

        public void LevelGameOver(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "GameOver");
        }

        public void LogEvent(string publisher, string target, string eventName)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Event))
            {
                Debug.Log($"Pub: {publisher} - Event: {eventName} - Target: {target}");
            }

        }

        public void LevelGameStarted(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "GameStarted");
        }

        public void EnemyHasDied(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "HasDied");
        }

        public void PlayerHasDied(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "HasDied");
        }

        public void PlayerHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            LogEvent(publisher.GetType().Name, target, "HealthLevelChanged");
        }

        public void OrchestrationCancelled(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "OrchestrationCancelled");
        }

        public void OrchestrationComplete(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "OrchestrationComplete");
        }

        public void OrchestrationStarted(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "OrchestrationStarted");
        }

        public void PlayerScoreMultiplierCollected(object publisher, string target, int scoreMultiplier)
        {
            LogEvent(publisher.GetType().Name, target, "ScoreMultiplierCollected");
        }

        private void PlayerScored(object publisher, string target, int score)
        {
            LogEvent(publisher.GetType().Name, target, $"PlayerScored ({score})");
        }

        private void PlayerWins(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "PlayerWins");
        }
        private void HighScoreChanged(object publisher, string target, int hiScore)
        {
            LogEvent(publisher.GetType().Name, target, $"HighScoreChanged ({hiScore})");
        }

        private void MultiplierChanged(object publisher, string target, int multiplier)
        {
            LogEvent(publisher.GetType().Name, target, $"MultiplierChanged ({multiplier})");
        }

        private void ScoreChanged(object publisher, string target, int score)
        {
            LogEvent(publisher.GetType().Name, target, $"ScoreChanged ({score})");
        }
    }
}
