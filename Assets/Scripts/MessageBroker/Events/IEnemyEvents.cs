using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class PlayerScored : UnityEvent<object, string, int> { }

    /// <summary>
    /// Interface required for a Enemy Events Publisher.
    /// </summary>
    public interface IEnemyEventsPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasDied(object publisher, string target);

        void PublishPlayerScored(object publisher, string target, int score);
    }

    /// <summary>
    /// Interface required by the Message Broker to support messages published by a <see cref="IEnemyEventsPublisher"/>
    /// </summary>
    public interface IEnemyEventsMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }

        PlayerScored PlayerScored { get; }
    }
}
