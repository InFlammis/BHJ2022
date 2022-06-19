using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IPlayerEventsPublisher, IPlayerEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _Player_HasDied = new HasDied();
        [SerializeField] private HealthLevelChanged _Player_HealthLevelChanged;

        public event Func<object, string, Transform> RequestForPlayerTransformEvent;
        public event Func<object, string, bool> RequestForPlayerIsAliveEvent;


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

        bool IPlayerEventsPublisher.RequestForPlayerIsAlive(object publisher, string target)
        {
            return RequestForPlayerIsAliveEvent?.Invoke(publisher, target) ?? false;
        }
    }
}
