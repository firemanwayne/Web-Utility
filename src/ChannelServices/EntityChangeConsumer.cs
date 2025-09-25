namespace Web.Utility.ChannelServices;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Web.Utility.Interfaces;

internal sealed class EntityChangeConsumer(
    ChannelReader<IUpdateable> reader,
    ILogger<EntityChangeConsumer> logger) : EntityChangeConsumer<IUpdateable>(reader, logger), IChangeConsumer
{ }

internal class EntityChangeConsumer<T>(
    ChannelReader<T> reader,
    ILogger<EntityChangeConsumer<T>> logger) : IChangeConsumer<T>
{
    protected ConcurrentDictionary<Guid, Func<T, Task>> _subscriptions = [];

    public async Task StartListeningAsync(CancellationToken token = default)
    {
        try
        {
            logger.LogTrace("Consumer listening");

            await foreach (T item in reader.ReadAllAsync(token))
            {
                var tasks = _subscriptions.Values.Select(async handler =>
                {
                    try
                    {
                        await handler(item);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("Handler error: {args}", args: ex.ToString());
                    }
                }).ToList();

                await Task.WhenAll(tasks);
            }
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError("Consumer {args} > forced stop", args: ex.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError("{args}", args: ex.ToString());
        }
    }

    public Guid Subscribe(Func<T, Task> OnEntityChanged)
    {
        var id = Guid.NewGuid();

        _subscriptions.TryAdd(id, OnEntityChanged);

        logger.LogTrace("Consumer added: {args}", args: id);

        return id;
    }

    public bool Unsubscribe(Guid subscribeId)
    {
        return _subscriptions.TryRemove(subscribeId, out _);
    }
}