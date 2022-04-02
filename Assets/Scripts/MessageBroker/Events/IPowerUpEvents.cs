using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class PowerUpHealthCollected : UnityEvent<object, string, int> { }
    [Serializable] public class PowerUpMultiplierCollected : UnityEvent<object, string, int> { }


    public interface IHealthChargerEventsPublisher
    {
        void PublishHealthCollected(object publisher, string target, int health);
    }

    public interface IHealthChargerEventsMessenger
    {
        PowerUpHealthCollected HealthCollected { get;}
    }

    public interface IScoreMultiplierEventsPublisher
    {
        void PublishScoreMultiplierCollected(object publisher, string target, int multiplier);
    }

    public interface IScoreMultiplierEventsMessenger
    {
        PowerUpMultiplierCollected MultiplierCollected { get; }
    }
}
