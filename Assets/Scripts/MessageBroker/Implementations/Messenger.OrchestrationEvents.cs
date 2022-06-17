using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IOrchestrationEventsPublisher, IOrchestrationEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private OrchestrationStarted _Orchestration_OrchestrationStarted = new OrchestrationStarted();
        [SerializeField] private OrchestrationCancelled _Orchestration_OrchestrationCancelled = new OrchestrationCancelled();
        [SerializeField] private OrchestrationComplete _Orchestration_OrchestrationComplete = new OrchestrationComplete();

        OrchestrationStarted IOrchestrationEventsMessenger.OrchestrationStarted => _Orchestration_OrchestrationStarted;
        OrchestrationCancelled IOrchestrationEventsMessenger.OrchestrationCancelled => _Orchestration_OrchestrationCancelled;
        OrchestrationComplete IOrchestrationEventsMessenger.OrchestrationComplete => _Orchestration_OrchestrationComplete;

        void IOrchestrationEventsPublisher.PublishOrchestrationCancelled(object publisher, string target)
        {
            _Orchestration_OrchestrationCancelled.Invoke(publisher, target);
        }

        void IOrchestrationEventsPublisher.PublishOrchestrationStarted(object publisher, string target)
        {
            _Orchestration_OrchestrationStarted.Invoke(publisher, target);
        }

        void IOrchestrationEventsPublisher.PublishOrchestrationComplete(object publisher, string target)
        {
            _Orchestration_OrchestrationComplete.Invoke(publisher, target);
        }
    }
}
