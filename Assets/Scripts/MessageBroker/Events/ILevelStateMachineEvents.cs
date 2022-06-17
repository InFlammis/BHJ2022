using System;
using UnityEngine.Events;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class LevelChangeStateRequest : UnityEvent<object, string, Managers.Levels.StateMachine.State> { }

    public interface ILevelStateMachineEventsPublisher
    {
        void PublishLevelChangeStateRequest(object publisher, string target, Managers.Levels.StateMachine.State state);
    }

    public interface ILevelStateMachineEventsMessenger
    {
        LevelChangeStateRequest ChangeStateRequest { get; }
    }
}
