using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : ISpitEventsPublisher, ISpitEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _Spit_HasDied = new HasDied();

        HasDied ISpitEventsMessenger.HasDied => _Spit_HasDied;

        void ISpitEventsPublisher.PublishSpitHasDied(object publisher, string target)
        {
            _Spit_HasDied.Invoke(publisher, target);
        }
    }
}
