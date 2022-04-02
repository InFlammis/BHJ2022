using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class GameOver : UnityEvent<object, string> { }
    [Serializable] public class GameStarted : UnityEvent<object, string> { }
    [Serializable] public class PlayerWins : UnityEvent<object, string> { }

    public interface ILevelEventsPublisher
    {
        void PublishGameOver(object publisher, string target);
        void PublishGameStarted(object publisher, string target);
        void PublishPlayerWins(object publisher, string target);

    }

    public interface ILevelEventsMessenger
    {
        GameOver GameOver { get; }
        GameStarted GameStarted { get; }
        PlayerWins PlayerWins { get; }
    }
}
