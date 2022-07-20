namespace InFlammis.Victoria.Assets.Scripts.MessageBroker.Events
{
    public interface ISpitEventsPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishSpitHasDied(object publisher, string target);
    }

    public interface ISpitEventsMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }
    }
}
