namespace DivinitySoftworks.Core.Net.EventBus;

/// <summary>
/// Defines a contract for asynchronously publishing messages to an event bus.
/// </summary>
public interface IPublisher {
    /// <summary>
    /// Publishes a message of type <typeparamref name="T"/> to the event bus
    /// and returns a result of type <typeparamref name="R"/> asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the message to publish.</typeparam>
    /// <typeparam name="R">The type of the result returned after publishing.</typeparam>
    /// <param name="busName">The name of the event bus (channel) to publish the message to.</param>
    /// <param name="message">The message to publish.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a result of type <typeparamref name="R"/>.
    /// </returns>
    Task<R> PublishAsync<T, R>(string busName, T message);
}
