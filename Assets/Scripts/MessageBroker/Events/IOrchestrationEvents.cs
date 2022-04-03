using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class OrchestrationStarted : UnityEvent<object, string> { }
    [Serializable] public class OrchestrationCancelled : UnityEvent<object, string> { }
    [Serializable] public class OrchestrationComplete : UnityEvent<object, string> { }

    public interface IOrchestrationEventsPublisher
    {
        void PublishOrchestrationStarted(object publisher, string target);
        void PublishOrchestrationCancelled(object publisher, string target);
        void PublishOrchestrationComplete(object publisher, string target);
    }

    public interface IOrchestrationEventsMessenger
    {
        OrchestrationStarted OrchestrationStarted { get; }
        OrchestrationCancelled OrchestrationCancelled { get; }
        OrchestrationComplete OrchestrationComplete { get; }
    }
}
