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
        services.AddSingleton<IDataStorage, DevNullDataStorage>();

        return services;
    }
    //public static IServiceCollection ConfigureSnaasCentralServices(this IServiceCollection services, IConfiguration configuration)
    //{
    //    var x = configuration.GetSection("users");
    //    services.Configure<UserOptions>(configuration.GetSection("users"));

    //    services.Configure<EmailSettings>(configuration.GetSection("sensenet:Email"));
    //    services.Configure<NotificationOptions>(configuration.GetSection("sensenet:SNaaS:Notification"));
    //    services.Configure<RepositorySupervisorSettings>(configuration.GetSection("sensenet:SNaaS:RepositorySupervisor"));

    //    services.ConfigureRepositoryType(options => { options.RepositoryType = "snaascentral"; });

    //    return services;
    //}
}