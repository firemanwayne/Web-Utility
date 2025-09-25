namespace Web.Utility.Interfaces;


using System.Threading;

/// <summary>
/// Service that listens for messages that are sent using channels
/// </summary>
public interface IChangeConsumer<T>
{
    /// <summary>
    /// Starting listening for changes.
    /// </summary>
    /// <param name="token">Cancellation Token used to stop listening for changes</param>
    /// <returns></returns>
    Task StartListeningAsync(CancellationToken token = default);

    Guid Subscribe(Func<T, Task> OnEntityChanged);

    bool Unsubscribe(Guid subscribeId);
}

public interface IChangeConsumer : IChangeConsumer<IUpdateable>
{ }