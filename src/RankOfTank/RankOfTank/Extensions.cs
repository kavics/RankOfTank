using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RankOfTank;

[ExcludeFromCodeCoverage]
public static class Extensions
{
    public static IServiceCollection AddRankOfTank(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSingleton<IRoTController, RoTController>();
        services.AddSingleton<IWotConnector, WotConnector>();
        services.AddSingleton<IUserStore, UserStore>();
        services.AddSingleton<IDataLoader, WebLoader>();
        services.AddSingleton<IDataStorage, InMemoryDataStorage>();

        return services;
    }
}