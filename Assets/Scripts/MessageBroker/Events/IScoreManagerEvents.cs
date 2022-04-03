using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class ScoreChanged : UnityEvent<object, string, int> { }
    [Serializable] public class MultiplierChanged : UnityEvent<object, string, int> { }
    [Serializable] public class HiScoreChanged : UnityEvent<object, string, int> { }

    public interface IScoreManagerEventsPublisher
    {
        void PublishScoreChanged(object publisher, string target, int score);
        void PublishMultiplierChanged(object publisher, string target, int multiplier);
        void PublishHiScoreChanged(object publisher, string target, int hiScore);
    }

    public interface IScoreManagerEventsMessenger
    {
        ScoreChanged ScoreChanged { get; }
        MultiplierChanged MultiplierChanged { get; }
        HiScoreChanged HiScoreChanged { get; }
    }
}
