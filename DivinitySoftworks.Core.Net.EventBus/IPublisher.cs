namespace DivinitySoftworks.Core.Net.EventBus;

/// <summary>
/// Defines a contract for publishing messages to an event bus.
/// </summary>
public interface IPublisher {
    /// <summary>
    /// Publishes a message of type <typeparamref name="T"/> to the event bus
    /// and returns a result of type <typeparamref name="R"/>.
    /// </summary>
    /// <typeparam name="T">The type of the message to publish.</typeparam>
    /// <typeparam name="R">The type of the result returned after publishing.</typeparam>
    /// <param name="message">The message to publish.</param>
    /// <returns>A result of type <typeparamref name="R"/>.</returns>
    R PublishAsync<T, R>(T message);
}
