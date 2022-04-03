using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IHudEventsPublisher, IHudEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private SetCentralMessage _Hud_SetCentralMessage = new SetCentralMessage();

        SetCentralMessage IHudEventsMessenger.SetCentralMessage => _Hud_SetCentralMessage;

        void IHudEventsPublisher.PublishSetCentralMessage(object publisher, string target, string message)
        {
            _Hud_SetCentralMessage.Invoke(publisher, target, message);
        }
    }
}
