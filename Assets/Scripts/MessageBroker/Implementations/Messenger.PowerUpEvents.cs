using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : 
        IHealthChargerEventsPublisher, IHealthChargerEventsMessenger,
        IScoreMultiplierEventsPublisher, IScoreMultiplierEventsMessenger
    { }

    public partial class Messenger
    {
        [SerializeField] private PowerUpHealthCollected _PowerUp_HealthCollected = new PowerUpHealthCollected();
        [SerializeField] private PowerUpMultiplierCollected _PowerUp_MultiplierCollected = new PowerUpMultiplierCollected();

        PowerUpHealthCollected IHealthChargerEventsMessenger.HealthCollected { get; } = new PowerUpHealthCollected();
        PowerUpMultiplierCollected IScoreMultiplierEventsMessenger.MultiplierCollected { get; } = new PowerUpMultiplierCollected();

        void IHealthChargerEventsPublisher.PublishHealthCollected(object publisher, string target, int health)
        {
            _PowerUp_HealthCollected.Invoke(publisher, target, health);
        }
        void IScoreMultiplierEventsPublisher.PublishScoreMultiplierCollected(object publisher, string target, int multiplier)
        {
            _PowerUp_MultiplierCollected.Invoke(publisher, target, multiplier);
        }

    }
}
