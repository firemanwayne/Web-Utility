using Microsoft.Extensions.Hosting;

using Web.Utility.Builders;

namespace Web.Utility;

public static class ServiceRegistryExtensions
{
    /// <summary>
    /// Registers services for Sample Domain Management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static WebUtilityBuilder AddWebUtilityBuilder(this IHostApplicationBuilder services)
    {
        return new WebUtilityBuilder(services);
    }
}
