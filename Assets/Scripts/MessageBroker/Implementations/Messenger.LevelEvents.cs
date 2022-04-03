using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : ILevelEventsPublisher, ILevelEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private GameOver _Level_GameOver = new GameOver();
        [SerializeField] private GameStarted _Level_GameStarted = new GameStarted();
        [SerializeField] private PlayerWins _Level_PlayerWins = new PlayerWins();

        GameOver ILevelEventsMessenger.GameOver => _Level_GameOver;
        GameStarted ILevelEventsMessenger.GameStarted => _Level_GameStarted;
        PlayerWins ILevelEventsMessenger.PlayerWins => _Level_PlayerWins;

        void ILevelEventsPublisher.PublishGameOver(object publisher, string target)
        {
            _Level_GameOver.Invoke(publisher, target);
        }

        void ILevelEventsPublisher.PublishGameStarted(object publisher, string target)
        {
            _Level_GameStarted.Invoke(publisher, target);
        }

        void ILevelEventsPublisher.PublishPlayerWins(object publisher, string target)
        {
            _Level_PlayerWins.Invoke(publisher, target);
        }
    }
}
