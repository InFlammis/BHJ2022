using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IPlayerEventsPublisher, IPlayerEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _Player_HasDied = new HasDied();
        [SerializeField] private HealthLevelChanged _Player_HealthLevelChanged;

        public event Func<object, string, Transform> RequestForPlayerTransformEvent;


        HasDied IPlayerEventsMessenger.HasDied  => _Player_HasDied;

        HealthLevelChanged IPlayerEventsMessenger.HealthLevelChanged => _Player_HealthLevelChanged;

        void IPlayerEventsPublisher.PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            _Player_HealthLevelChanged.Invoke(publisher, target,healthLevel, maxHealthLevel);
        }

        void IPlayerEventsPublisher.PublishHasDied(object publisher, string target)
        {
            _Player_HasDied.Invoke(publisher, target);
        }

        Transform IPlayerEventsPublisher.RequestForPlayerTransform(object publisher, string target)
        {
            return RequestForPlayerTransformEvent?.Invoke(publisher, target);
        }
    }
}
