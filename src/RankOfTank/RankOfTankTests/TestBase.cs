using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankOfTank;

namespace RankOfTankTests;

public class TestBase
{
    public ServiceProvider BuildServices(string configFileName, Action<ServiceCollection>? callback = null)
    {
        // build config
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configFileName, optional: false)
            .Build();

        var services = new ServiceCollection();
        services.AddOptions<UserOptions>().Bind(configuration.GetSection("App"));
        services.AddOptions<AccessOptions>().Bind(configuration.GetSection("App"));

        if (callback != null)
            callback(services);

        return services.BuildServiceProvider();
    }

    public static string GetFileContent(string fileName)
    {
        using var reader = new StreamReader(fileName);
        return reader.ReadToEnd();
    }
}
