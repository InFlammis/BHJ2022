using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker.Events
{
    //public delegate Func<Transform> RequestForPlayerTransform();
    /// <summary>
    /// Interface required for a Player Events Publisher.
    /// </summary>
    public interface IPlayerEventsPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasDied(object publisher, string target);

        /// <summary>
        /// Publish an event of type <see cref="HasSpawned"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasSpawned(object publisher, string target);

        /// <summary>
        /// Publish an event of type <see cref="HealthLevelChanged"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="healthLevel">The new health level.</param>
        /// <param name="maxHealthLevel">Maximum health level.</param>
        void PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel);

        Transform RequestForPlayerTransform(object publisher, string target);

        bool RequestForPlayerIsAlive(object publisher, string target);

    }

    /// <summary>
    /// Interface required by the Message Broker to support messages published by a <see cref="IPlayerEventsPublisher"/>
    /// </summary>
    public interface IPlayerEventsMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }

        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasSpawned"/>, to subscribe to.
        /// </summary>
        HasSpawned HasSpawned { get; }

        /// <summary>
        /// Returns a reference to a delegate of type <see cref="ReceivedDamage"/>, to subscribe to.
        /// </summary>
        HealthLevelChanged HealthLevelChanged { get; }

        event Func<object, string, Transform> RequestForPlayerTransformEvent;

        event Func<object, string, bool> RequestForPlayerIsAliveEvent;

    }
}
