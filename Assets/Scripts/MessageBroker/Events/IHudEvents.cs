using System;
using UnityEngine.Events;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class SetCentralMessage : UnityEvent<object, string, string> { }

    public interface IHudEventsPublisher
    {
        void PublishSetCentralMessage(object publisher, string target, string message);
    }

    public interface IHudEventsMessenger
    {
        SetCentralMessage SetCentralMessage { get; }
    }
}
