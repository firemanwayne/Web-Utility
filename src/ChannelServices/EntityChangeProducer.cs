namespace Web.Utility.ChannelServices;

using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Web.Utility.Interfaces;

internal sealed class EntityChangeProducer(ChannelWriter<IUpdateable> writer) : IChangeProducer
{
    public async Task PushAsync(IUpdateable entity, CancellationToken token = default)
    {
        await writer.WriteAsync(entity, token);
    }
}

internal sealed class EntityChangeProducer<T>(ChannelWriter<T> writer) : IChangeProducer<T>
{
    public async Task PushAsync(T entity, CancellationToken token = default)
    {
        await writer.WriteAsync(entity, token);
    }
}
