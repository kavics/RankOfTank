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
        //services.AddInMemoryDataStorage();
        services.AddFsDataStorage();
        //services.AddFsDataStorage(options => options.DatabaseDirectory = @"D:\RankOfTanksData");

        return services;
    }

    public static IServiceCollection AddInMemoryDataStorage(this IServiceCollection services)
    {
        return services.AddSingleton<IDataStorage, InMemoryDataStorage>();
    }
    public static IServiceCollection AddFsDataStorage(this IServiceCollection services, Action<FsDataStorageOptions>? configure = null)
    {
        services.AddSingleton<IDataStorage, FsDataStorage>();
        services.Configure<FsDataStorageOptions>(x => { configure?.Invoke(x); });

        return services;
    }
}