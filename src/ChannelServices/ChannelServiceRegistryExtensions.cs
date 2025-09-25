namespace Web.Utility.ChannelServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Threading.Channels;

using Web.Utility.Interfaces;

public static class ChannelServiceRegistryExtensions
{
    /// <summary>
    /// Registers infrastructure required for <see cref="IChangeProducer"/> and <see cref="IChangeConsumer"/>.
    /// <see cref="IChangeProducer"/> and <see cref="IChangeConsumer"/> are interfaces that push and receive changes that are submitted to APIM
    /// using unbound <see cref="Channel"/>(s) usable by any number of readers and writers concurrently. Using this will allow notifications when 
    /// entities successfully change, allowing you to react to those changes.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddEntityChangeServices(this IServiceCollection services)
    {
        services.AddSingleton(Channel.CreateUnbounded<IUpdateable>());

        services.AddSingleton<IChangeProducer>(ctx =>
        {
            Channel<IUpdateable> channel = ctx.GetRequiredService<Channel<IUpdateable>>();

            return new EntityChangeProducer(channel);
        });

        services.AddSingleton<IChangeConsumer>(ctx =>
        {
            Channel<IUpdateable> channel = ctx.GetRequiredService<Channel<IUpdateable>>();
            ILogger<EntityChangeConsumer> logger = ctx.GetRequiredService<ILogger<EntityChangeConsumer>>();

            return new EntityChangeConsumer(channel, logger);
        });

        return services;
    }

    public static IServiceCollection AddEntityChangeServicesFor<T>(this IServiceCollection services)
    {
        services.AddSingleton(Channel.CreateUnbounded<T>());

        services.AddSingleton<IChangeProducer<T>>(ctx =>
        {
            Channel<T> channel = ctx.GetRequiredService<Channel<T>>();

            return new EntityChangeProducer<T>(channel);
        });

        services.AddSingleton<IChangeConsumer<T>>(ctx =>
        {
            Channel<T> channel = ctx.GetRequiredService<Channel<T>>();
            ILogger<EntityChangeConsumer<T>> logger = ctx.GetRequiredService<ILogger<EntityChangeConsumer<T>>>();

            return new EntityChangeConsumer<T>(channel, logger);
        });

        return services;
    }
}