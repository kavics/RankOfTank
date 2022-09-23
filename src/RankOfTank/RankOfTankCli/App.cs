using Microsoft.Extensions.Logging;
using RankOfTank;

namespace RankOfTankCli;

internal class App
{
    private readonly IRoTController _roTController;
    private readonly IUserStore _userStore;
    private readonly ILogger<App> _logger;

    public App(IRoTController roTController, IUserStore userStore, ILogger<App> logger)
    {
        _roTController = roTController;
        _userStore = userStore;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task RunAsync(string[] args, CancellationToken cancel)
    {
        _logger.LogInformation("Starting...");

        Console.WriteLine("Rank of Tanks");

        var userNames = _userStore.GetUserNames();
        Console.WriteLine("Known users ({0})", userNames.Length);
        foreach(var userName in userNames)
            Console.WriteLine("  {0}", userName);
        Console.WriteLine();

        var userData = await _roTController.GetUserDataAsync("gyebi5", cancel);

        _logger.LogInformation("Finished!");

        await Task.CompletedTask;
    }
}