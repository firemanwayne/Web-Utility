namespace Web.Utility.Builders;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public sealed class WebUtilityBuilder(IHostApplicationBuilder builder)
{
    public IServiceCollection Services { get; } = builder.Services;

    public IConfiguration Configuration { get; } = builder.Configuration;
}
