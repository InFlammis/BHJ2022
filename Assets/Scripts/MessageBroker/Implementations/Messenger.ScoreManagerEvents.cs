using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IScoreManagerEventsPublisher, IScoreManagerEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private ScoreChanged _ScoreManager_ScoreChanged = new ScoreChanged();
        [SerializeField] private MultiplierChanged _ScoreManager_MultiplierChanged = new MultiplierChanged();
        [SerializeField] private HiScoreChanged _ScoreManager_HiScoreChanged = new HiScoreChanged();

        ScoreChanged IScoreManagerEventsMessenger.ScoreChanged => _ScoreManager_ScoreChanged;

        MultiplierChanged IScoreManagerEventsMessenger.MultiplierChanged => _ScoreManager_MultiplierChanged;

        HiScoreChanged IScoreManagerEventsMessenger.HiScoreChanged => _ScoreManager_HiScoreChanged;

        void IScoreManagerEventsPublisher.PublishHiScoreChanged(object publisher, string target, int hiScore)
        {
            _ScoreManager_HiScoreChanged.Invoke(publisher, target, hiScore);
        }

        void IScoreManagerEventsPublisher.PublishMultiplierChanged(object publisher, string target, int multiplier)
        {
            _ScoreManager_MultiplierChanged.Invoke(publisher, target, multiplier);
        }

        void IScoreManagerEventsPublisher.PublishScoreChanged(object publisher, string target, int score)
        {
            _ScoreManager_ScoreChanged.Invoke(publisher, target, score);
        }
    }
}
