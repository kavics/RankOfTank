using Microsoft.Extensions.Logging;
using RankOfTank;
using RankOfTank.WotModels;

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
/*
        foreach (var userName in userNames)
        {
            var userData = await _roTController.GetUserDataAsync(userName, cancel).ConfigureAwait(false);
            WriteHeadData(userData);
        }

        Console.WriteLine("One more time:");

        foreach (var userName in userNames)
        {
            var userData = await _roTController.GetUserDataAsync(userName, cancel).ConfigureAwait(false);
            WriteHeadData(userData);
        }
*/
        Console.WriteLine("Garages:");
        foreach (var userName in userNames)
        {
            var garageData = await _roTController.GetUserGarageAsync(userName, cancel).ConfigureAwait(false);
            WriteGarageData(userName, garageData);
        }

        _logger.LogInformation("Finished!");

        await Task.CompletedTask;
    }


    private void WriteHeadData(WotUserData? userData)
    {
        if (userData == null)
        {
            Console.WriteLine("[null]");
            return;
        }
        Console.WriteLine($"{userData.nickname}:");
        Console.WriteLine($"  battles: {userData.statistics.all.battles}");
        Console.WriteLine($"  last battle: {userData.last_battle_time.ToDateTime().ToLocalTime()}");
    }

    private void WriteGarageData(string userName, WotGarageData? garageData)
    {
        Console.WriteLine($"{userName}:");

        if (garageData == null)
            return;
        Console.Write($"  TankIds ({garageData.TankIds.Length}):");
        foreach (var tankId in garageData.TankIds)
            Console.Write($" {tankId}");
        Console.WriteLine();
    }
}