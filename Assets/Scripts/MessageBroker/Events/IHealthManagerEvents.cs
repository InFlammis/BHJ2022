using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{

    /// <summary>
    /// Interface required for a HealthManager Events Publisher.
    /// </summary>
    public interface IHealthManagerEventsPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasDied(object publisher, string target);

        /// <summary>
        /// Publish an event of type <see cref="HealthLevelChanged"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="healthLevel">The new health level.</param>
        /// <param name="maxHealthLevel">Maximum health level.</param>
        void PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel);
    }

    /// <summary>
    /// Interface required by the Message Broker to support messages published by a <see cref="IHealthManagerEventsPublisher"/>
    /// </summary>
    public interface IHealthManagerEventsMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }

        /// <summary>
        /// Returns a reference to a delegate of type <see cref="ReceivedDamage"/>, to subscribe to.
        /// </summary>
        HealthLevelChanged HealthLevelChanged { get; }
    }
}
