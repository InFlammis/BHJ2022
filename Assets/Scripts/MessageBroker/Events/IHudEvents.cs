using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
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
