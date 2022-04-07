using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : ILevelStateMachineEventsPublisher, ILevelStateMachineEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private LevelChangeStateRequest _Level_StateMachine_ChangeStateRequest = new LevelChangeStateRequest();

        LevelChangeStateRequest ILevelStateMachineEventsMessenger.ChangeStateRequest => _Level_StateMachine_ChangeStateRequest;

        void ILevelStateMachineEventsPublisher.PublishLevelChangeStateRequest(object publisher, string target, Managers.Levels.StateMachine.State state)
        {
            _Level_StateMachine_ChangeStateRequest.Invoke(publisher, target, state);
        }
    }
}
