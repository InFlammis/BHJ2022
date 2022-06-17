using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IHealthManagerEventsPublisher, IHealthManagerEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _HealthManager_HasDied = new HasDied();
        [SerializeField] private HealthLevelChanged _HealthManager_HealthLevelChanged = new HealthLevelChanged();

        HasDied IHealthManagerEventsMessenger.HasDied => _HealthManager_HasDied;
        HealthLevelChanged IHealthManagerEventsMessenger.HealthLevelChanged => _HealthManager_HealthLevelChanged;

        void IHealthManagerEventsPublisher.PublishHasDied(object publisher, string target)
        {
            _HealthManager_HasDied.Invoke(publisher, target);
        }

        void IHealthManagerEventsPublisher.PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            _HealthManager_HealthLevelChanged.Invoke(publisher, target, healthLevel, maxHealthLevel);
        }
    }
}
