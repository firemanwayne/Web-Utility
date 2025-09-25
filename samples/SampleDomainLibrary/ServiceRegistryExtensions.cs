namespace SampleDomainLibrary;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using SampleDomainLibrary.Options;

using Web.Utility.Builders;

public static class ServiceRegistryExtensions
{
    /// <summary>
    /// Registers services for Sample Domain Management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static WebUtilityBuilder AddSampleDomainManagement(this WebUtilityBuilder builder)
    {
        SampleDomainOptions opts = new();

        builder
            .Configuration
            .Bind(SampleDomainOptions.AppConfigKey, opts);

        builder.Services.TryAddSingleton(Microsoft.Extensions.Options.Options.Create(opts));

        return builder;
    }
}
