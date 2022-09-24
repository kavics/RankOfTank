using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RankOfTank;
using RankOfTankCli;

static void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.AddDebug();
        builder.SetMinimumLevel(LogLevel.Trace);
    });

    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", optional: false)
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables()
        .Build();

    services.AddOptions<UserOptions>().Bind(configuration.GetSection("App"));
    services.AddOptions<AccessOptions>().Bind(configuration.GetSection("App"));
    services.AddRankOfTank(configuration);

    services.AddTransient<App>();
}

var services = new ServiceCollection();
ConfigureServices(services);

await using var serviceProvider = services.BuildServiceProvider();
await serviceProvider.GetRequiredService<App>()
    .RunAsync(args, CancellationToken.None).ConfigureAwait(false);